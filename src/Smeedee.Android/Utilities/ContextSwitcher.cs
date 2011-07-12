using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Smeedee.Model;

namespace Smeedee.Utilities
{
    class ContextSwitcher
    {
        private List<Action> actions;
        private readonly Activity activity;
        private IBackgroundWorker backgroundWorker;
        private int current = 0;
        private bool hasStarted = false;

        public ContextSwitcher(Activity activity)
        {
            this.activity = activity;
            this.actions = new List<Action>();
            this.backgroundWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
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

        public void Do()
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
