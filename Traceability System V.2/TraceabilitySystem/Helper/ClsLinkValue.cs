namespace TraceabilitySystem
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class ClsLinkValue
    {
        private List<ClsScanTagID> _ArrayScanTagID = new List<ClsScanTagID>();
        public List<ClsScanTagID> p_ArrayScanTagID =>
           this._ArrayScanTagID;


        public class ClsScanTagID
        {
            public string EPC_Value = "";
            public string PC_EPC_Val = "";
        }

       

    }
}

