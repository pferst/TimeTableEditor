using System;
using System.IO;

namespace TimetableEditor
{
    class Plan
    {
        //fields
        private string fileContent, targetFile = "", defFName;
        private bool notFounded = true, began = false;
        private static bool isExecuted = false;

        //properties
        private string FileContent
        {
            get
            {
                return fileContent;
            }
            set
            {
                fileContent = value;
            }
        }
        private string TargetFile
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(targetFile))
                {
                    return targetFile;
                }
                return "empty data";
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && value != targetFile && value.EndsWith(".ics"))
                {
                    targetFile = value;
                }
            }
        }
        //constructors
        public Plan(string fileName)
        {
            TargetFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\" + fileName + ".ics";
            defFName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\" + fileName;
        }

        //methods
        public static bool FileExists(string fileName)
        {
            return File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\" + fileName + ".ics");
        }
        public void RemoveSubject(string subName)
        {
            if (File.Exists(this.TargetFile))
            {
                Console.WriteLine("File found");
                StreamReader reader = new StreamReader(this.TargetFile);
                string temp;
                while (reader.Peek() != -1)
                {
                    temp = reader.ReadLine();
                    if (!began)
                    {
                        if (temp.StartsWith("BEGIN:VEVENT"))
                        {
                            this.began = true;
                        }
                        else
                        {
                            SaveToNewFile(temp);
                        }
                    }
                    if (temp.StartsWith("END:") && this.began)
                    {
                        if (this.notFounded)
                        {
                            SaveToNewFile(this.FileContent + temp);
                        }
                        this.FileContent = "";
                        this.notFounded = true;
                    }
                    else if (this.began)
                    {
                        if (temp.ToUpper().Contains("SUMMARY:" + subName.Trim().ToUpper()))
                        {
                            this.notFounded = false;
                        }
                        else if (this.notFounded)
                        {
                            this.FileContent += temp + "\n";
                        }
                    }
                }
                reader.Close();
                this.began = false;
                MoveFinalFile();
                Console.WriteLine("Finished");
            }
            else
            {
                Console.WriteLine("File didn't find - {0}", this.TargetFile);
            }
        }
        private void SaveToNewFile(string content)
        {
            string destFile = "temp.ics";
            StreamWriter writer;
            if (!isExecuted)
            {
                isExecuted = true;
                writer = new StreamWriter(destFile);
            }
            else
            {
                writer = new StreamWriter(destFile, true);
            }
            writer.WriteLine(content);
            writer.Flush();
            writer.Close();
        }
        private void MoveFinalFile()
        {
            this.TargetFile = this.defFName + "_Edited.ics";
            File.Move("temp.ics", this.TargetFile, true);
        }
    }
}
