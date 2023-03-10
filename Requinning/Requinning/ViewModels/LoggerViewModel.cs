using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.ViewModels
{
    public class LoggerViewModel: BaseViewModel
    {
        private string _log = "loaded";
        public string Log
        {
            get { return (_log); }
            set { SetProperty(ref _log, value); }
        }

        private List<string> logs { get; set; }

        public LoggerViewModel()
        {
            logs = new List<string>();
        }
        
        public void Record(string message)
        {
            this.Log = message;
            logs.Add(message);
        }

    }
}
