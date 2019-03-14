using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Linq;
//using System.Text;

namespace ChequeItemDelete
{
    class Program
    {
        static void parselog (string fname)
        {
            if (fname=="")
            {
                return;
            }
            string readMeText="";
            using (StreamReader readtext = new StreamReader(Path.Combine(@"c:\temp\logs", fname)))
            {
                //readMeText = readtext.ReadLine();
                readMeText = readtext.ReadLine();
                
            }

            string start = "ePlus.ARMCommon.Log.ARMLogger";
            string end = "[ID_CASH_REGISTER]";
            bool deleted = false;
            List<string> LogList = readMeText.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            LogList = File.ReadAllLines(Path.Combine(@"c:\temp\logs", fname)).ToList();
            StringBuilder sb = new StringBuilder();
            Console.WriteLine(fname);

            bool startparse = false;
            foreach (string str in LogList)
            {
                // Console.WriteLine(str);
              

                    if (str.Contains(start) && startparse == false)
                {
                    startparse = true;
                    sb = new StringBuilder();
                    deleted = false;
                }

                if (startparse == true)
                {
                    Console.WriteLine(str);
                    if (str.Contains("ChequeItemDelete"))
                        {
                        deleted = true;
                        }
                    if (str.Contains("[DATE]"))
                        {
                        sb.AppendLine(str);
                        }
                    if (str.Contains("[GOODS_NAME]"))
                    {
                        sb.AppendLine(str);
                    }
                    if (str.Contains("[QUANTITY]"))
                    {
                        sb.AppendLine(str);
                    }
                    if (str.Contains("[SUMM]"))
                    {
                        sb.AppendLine(str);
                    }
                    if (str.Contains("[USER_FULL_NAME]"))
                    {
                        sb.AppendLine(str);
                    }

                    if (str.Contains(end)&& deleted==true)
                    {
                        startparse = false;
                        deleted = false;
                        sb.AppendLine("------------------------------------------------");
                        if (sb.ToString().Contains("Кузьмина"))
                        {
                            File.AppendAllText(@"c:\temp\logs\result.txt", sb.ToString() + "\r\n");
                        }
                        
                        sb = new StringBuilder();
                    }
                }
                if (str.Contains(end))
                {
                    startparse = false;
                    deleted = false;
                }
            }

        }
        
        static void Main(string[] args)
        {

            if (File.Exists(@"c:\temp\logs.result.txt"))
                File.Delete(@"c:\temp\logs.result.txt");
            string dir = @"c:\temp\logs";
            DirectoryInfo d = new DirectoryInfo(dir);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.log"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                //str = str + ", " + file.Name;
                parselog(file.Name);
            }
            /*

            using (StreamWriter writetext = new StreamWriter("write.txt"))
            {
                writetext.WriteLine("writing in text file");
            }
            */
            

        }
    }
}

/*
 [STATE] = ''
[DURATION] = '0'
[ID_CHEQUE_GLOBAL] = 'f4ba423f-cef9-4253-b3a7-4ba90c3d28c6'
[DATE] = '19.02.2019 12:06:31'
[CODE_OP] = 'ChequeItemDelete'
[ID_GOODS_GLOBAL] = '00000000-0000-0000-0000-000000000000'
[GOODS_NAME] = 'Диклофенак гель 5% 50г'
[QUANTITY] = '1'
[SUMM] = '115'
[DISCOUNT] = '0'
[ID_LOT_GLOBAL] = '4ae1e087-06e7-44cb-aa26-f9f3ac4cc82c'
[ID_USER_GLOBAL] = '5d7aa0d3-0c69-432a-8393-e3ceae88939d'
[USER_FULL_NAME] = 'Кузьмина Надежда'
[ID_CONTRACTOR_GLOBAL] = '1fb6f806-63bc-4ef0-afca-736c5dc1e7fa'
[ID_STORE_GLOBAL] = '00000000-0000-0000-0000-000000000000'
[COMMENT] = 'Удаление позиции чека'
[ID_USER_LOG_GLOBAL] = '4a4d9fd1-8cf4-411b-8c08-7c7ef01d13ba'
[ID_CASH_REGISTER] = '49'


    */
