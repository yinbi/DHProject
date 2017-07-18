using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGame.WebAdmin.Models
{
    public class ReturnMsg
    {
        private string _statusCode = "";
        private string _message = "";
        private string _navTabId = "";
        private string _rel = "";
        private string _callbackType = "";
        private string _forwardUrl = "";

        public string statusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string navTabId {
            get { return _navTabId; }
            set { _navTabId = value; }
        }
        public string rel {
            get { return _rel; }
            set { _rel = value; }
        }
        public string callbackType {
            get { return _callbackType; }
            set { _callbackType = value; }
        }
        public string forwardUrl
        {
            get { return _forwardUrl; }
            set { _forwardUrl = value; }
        }
    }
}