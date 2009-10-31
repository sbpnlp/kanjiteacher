using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections;
using System.Text;
using System.Diagnostics;
//using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.DAL
{
    public class IsoStorage : IIntermediateStorage
    {
        public static void IsoStorageDemo()
        {
            IsoStorage.WriteStorage();
            for (int i = 0; i < 4; i++)
                IsoStorage.ReadStorage();
        }

        public static void WriteStorage()
        {
            //Assembly/Machine store
            //IsolatedStorageFile myStorage = IsolatedStorageFile.GetMachineStoreForAssembly();

            //Assembly/User store

            IsolatedStorageFile myStorage = IsolatedStorageFile.GetUserStoreForAssembly();

            myStorage.CreateDirectory("TestDirectory");

            //create an isolated storage file stream
            IsolatedStorageFileStream userStream =
                new IsolatedStorageFileStream("UserSettings.dat"
                    , FileMode.Create
                    , myStorage);

            IsolatedStorageFileStream userStream2 =
                new IsolatedStorageFileStream(
                    Path.Combine("TestDirectory", "TestDirUserSettings.dat")
                    , FileMode.Create
                    , myStorage);

            //wrap IsolatedStorageFileStream to a StreamWriter
            StreamWriter writer = new StreamWriter(userStream2);
            writer.WriteLine("und das hier kommt ins testdirectory: UserName = Steven");
            writer.Close();
            writer = new StreamWriter(userStream);
            writer.WriteLine("usersettings ins usersettings-file");
            writer.Close();
        }

        public static void ReadStorage()
        {
            IsolatedStorageFile myStorage = IsolatedStorageFile.GetUserStoreForAssembly();

            string[] files = myStorage.GetFileNames("UserSettings.dat");
            if (files.Length == 0)
                Console.WriteLine("File not found");
            else
            {
                Console.WriteLine("File found");
                IsolatedStorageFileStream mynewuserStream =
                    new IsolatedStorageFileStream("UserSettings.dat", FileMode.Open, myStorage);
                StreamReader sr = new StreamReader(mynewuserStream);
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();
            }

            string[] directories = myStorage.GetDirectoryNames("TestDirectory");
            if (directories.Length == 0)
            {
                Console.WriteLine("Directory not found");
            }
            else
            {
                string[] dirfiles = myStorage.GetFileNames(Path.Combine("TestDirectory", "TestDirUserSettings.dat"));
                if (dirfiles.Length != 0)
                {
                    StreamReader sreader = new StreamReader(
                        new IsolatedStorageFileStream(
                            Path.Combine("TestDirectory", "TestDirUserSettings.dat"),
                            FileMode.Open, myStorage));
                    Console.WriteLine(sreader.ReadToEnd());
                    sreader.Close();
                }
                else Console.WriteLine("File not found");
            }
            myStorage.Remove();
        }
    }
}
