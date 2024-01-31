using IronPython.Hosting;
using Microsoft.Scripting.Runtime;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keras;
using Keras.Models;

namespace GenderApp
{
    public class PythonUtils
    {
        private static PythonUtils instance;

        public static PythonUtils Instance
        {
            get { if (instance == null) instance = new PythonUtils(); return PythonUtils.instance; }
            private set { PythonUtils.instance = value; }
        }

        public string run(string fullFilePath)
        {
            /*var model = Keras.Models.Model.LoadModel("D:\\cd_data_C\\Desktop\\App\\model.h5");*/
            /*var model = new Sequential();*/
            string a = "abc";
            return a; 
        }

        public string runPython(string fullFilePath)
        {
            /*/// engine 
            var engine = Python.CreateEngine();
            /// source 
            var scriptFile = @"D:\cd_data_C\Desktop\App\pythonfile.py"; 
                var source = engine.CreateScriptSourceFromFile(scriptFile);
            /// output stream 
            var eIO = engine.Runtime.IO;
                var errors = new MemoryStream();
                eIO.SetErrorOutput(errors, Encoding.Default);
                var results = new MemoryStream();
                eIO.SetOutput(results, Encoding.Default); 
            /// scope and set variable 
                var scope = engine.CreateScope();
            /// scope.SetVariable("image_path", fullFilePath);
            /// execute 
            source.Execute(scope);
            /// get variable 
            string res = string.Empty;
            res = scope.GetVariable("res");
            return res; */
            return run(fullFilePath);
        }
    }
}
