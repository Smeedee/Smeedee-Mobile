namespace Smeedee.Lib
{
    interface IFileIO
    {
        byte[] ReadAllBytes(string fileName);
        void WriteAllBytes(string fileName, byte[] bytes);
    }
}
