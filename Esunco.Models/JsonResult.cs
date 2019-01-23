using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esunco.Models
{

    public class JsonResultError
    {
        public bool Handled { get; set; }

        public string Message { get; set; }
    }

    public class JsonResult
    {
        public JsonResult(bool succeed)
        {
            this.Succeed = succeed;
        }

        public JsonResult()
            : this(true)
        {
        }
        public List<string> Messages
        {
            get;
            set;
        }


        public JsonResultError Error { get; set; }

        public bool Succeed { get; set; }
    }

    public class JsonResult<T> : JsonResult
    {

        public JsonResult()
            : base()
        {

        }
        public JsonResult(T result)
            : this()
        {
            this.Result = result;

        }

        public T Result { get; set; }
    }
}