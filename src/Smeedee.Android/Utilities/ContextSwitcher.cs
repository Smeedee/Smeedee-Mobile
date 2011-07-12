using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Smeedee.Model;

namespace Smeedee.Utilities
{
    public class ContextSwitcher
    {
        public static ContextSwitcher Using(Activity context)
        {
            return new ContextSwitcher(context);
        }


        private List<Action> actions = new List<Action>();
        private readonly Activity activity;
        private IBackgroundWorker backgroundWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
        private int current = 0;
        private bool hasStarted = false;

        private ContextSwitcher(Activity activity)
        {
            this.activity = activity;
        }

        public ContextSwitcher InBackground(Action fn)
        {
            actions.Add(() => backgroundWorker.Invoke(AddCallback(fn)));
            return this;
        }

        public ContextSwitcher InUI(Action fn)
        {
            actions.Add(() => activity.RunOnUiThread(AddCallback(fn)));
            return this;
        }

        private Action AddCallback(Action fn)
        {
            return () =>
                        {
                            fn();
                            Next();
                        };
        }

        public void Run()
        {
            if (hasStarted) throw new Exception("ContextSwitcher can only be run once");
            hasStarted = true;
            Next();
        }

        private void Next()
        {
            if (current >= actions.Count) return;
            var fn = actions[current];
            current += 1;
            fn();
        }
    }
}
