//'---------> Created by Joshy George <----------
//'---------> Use this as you like, no copyright, no restrictions <----------
//'---------> All I want is to Give, because I have Received  <----------
//'---------> So much help from others.Thank You ! <----------

///* This is my sample program for Converting data to Hex
// * Feel free to change or modify the code, and use it,it sure would be nice to
// * give me credit for something if you do use this in something that you make for yourself
// * or others, but what you do if up to you.
// * Questions or Comments may be addressed to Spot <joshygeo@gmail.com>, 
// * and you can find more  information on my web-site (http://www.joshygeo.tk).  
// * This code is free to use modify and distribute, so long as what your doing with it is also free, 
// * I'd like to here about any interesting modifications that other programmers come up with.

//'---------> Email:-  joshygeo@gmail.com <----------
//'---------> WebSite:- www.joshygeo.tk <----------

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic; // I'm using  this class for Hex Converion
using System.Windows.Forms;
namespace TraceabilitySystem
{
    public class HexConverter
    {
        public string Data_Hex_Asc(ref string Data ,TextBox txt )
        {
            try
            {

          
            string Data1 = "";
            string sData = "";

            while (Data.Length > 0)

            //first take two hex value using substring.
            //then  convert Hex value into ascii.
            //then convert ascii value into character.
            {
                Data1 = System.Convert.ToChar(System.Convert.ToUInt32(Data.Substring(0, 2), 16)).ToString();
                sData = sData + Data1;
                Data = Data.Substring(2, Data.Length - 2);
            }
            return sData;
            }
            catch (Exception)
            {
                txt.Focus();
                return null;
            }
        }
        public string Data_Asc_Hex(ref string Data,TextBox txt)
        {
            try
            {
                //first take each charcter using substring.
                //then  convert character into ascii.
                //then convert ascii value into Hex Format

                string sValue;
            string sHex = "";
            while (Data.Length > 0)
            {
                sValue = Conversion.Hex(Strings.Asc(Data.Substring(0, 1).ToString()));
                Data = Data.Substring(1, Data.Length - 1);
                sHex = sHex + sValue;
            }
            return sHex;
        }
            catch (Exception)
            {
                txt.Focus();
                return null;
            }
}


        public string Data_Asc_Hex2(ref string Data)
        {
            try
            {


                string Data1 = "";
                string sData = "";

                while (Data.Length > 0)

                //first take two hex value using substring.
                //then  convert Hex value into ascii.
                //then convert ascii value into character.
                {
                    Data1 = System.Convert.ToChar(System.Convert.ToUInt32(Data.Substring(0, 2), 16)).ToString();
                    sData = sData + Data1;
                    Data = Data.Substring(2, Data.Length - 2);
                }
                return sData;
            }
            catch (Exception)
            {
               
                return null;
            }
        }

    }
}
