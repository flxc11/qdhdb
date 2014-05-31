using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

namespace QDHServer.AppCode
{
    public class ResultData
    {
        private string result;

        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                if (this.result.Equals("0000"))
                    this.isErr = false;
                else
                    this.isErr = true;
                if (!this.result.Equals(""))
                    this.errCode = Convert.ToInt32(this.result);
            }
        }
        private string errMsg;

        public string ErrMsg
        {
            get { return errMsg; }
            set { errMsg = value; }
        }
        private int errCode;

        public int ErrCode
        {
            get { return errCode; }
            set
            {
                errCode = value;
                this.result = errCode.ToString("0000");
                if (errCode == 0)
                {
                    this.isErr = false;
                }
                else
                {
                    this.isErr = true;
                }
            }
        }
        private bool isErr;

        public bool IsErr
        {
            get { return isErr; }
            set { isErr = value; }
        }

        private string command;

        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        private string commandTxt;

        public string CommandTxt
        {
            get { return commandTxt; }
            set { commandTxt = value; }
        }

        private string fieldOne;

        public string FieldOne
        {
            get { return fieldOne; }
            set { fieldOne = value; }
        }
        private string fieldTwo;

        public string FieldTwo
        {
            get { return fieldTwo; }
            set { fieldTwo = value; }
        }
        private string fieldThree;

        public string FieldThree
        {
            get { return fieldThree; }
            set { fieldThree = value; }
        }

        private string fieldFour;

        public string FieldFour
        {
            get { return fieldFour; }
            set { fieldFour = value; }
        }
        public ResultData()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.result = "0";
            this.errMsg = "ok";
            this.command = "";
            this.commandTxt = "";
        }
    }
}
