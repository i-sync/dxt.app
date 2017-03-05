using System;
using System.Text;
using System.Globalization;
using System.Xml;

namespace Model
{
	/// <summary>
	///
	/// </summary>
	public class Cast {
        /// <summary>
        /// 二维码分隔符
        /// </summary>
        public static char Delimiter = '@';

		public static string HexEncode(byte[] aby) {
			string hex = "0123456789abcdef";
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < aby.Length; i++) {
				sb.Append(hex[(aby[i] & 0xf0) >> 4]);
				sb.Append(hex[aby[i] & 0x0f]);
			}
			return sb.ToString();
		}

		public static string EscapeSQL(string str) {
			str = str.Replace("\'", "\'\'");
			return str;
		}

		public static string EscapeSQLLike(string str) {
			str = str.Replace(@"\", @"\\");
			str = str.Replace("%", @"\%");
			str = str.Replace("_", @"\_");
			return str;
		}

		public static string EscapeJavaScript(string str) {
			str = str.Replace(@"\", @"\\");
			str = str.Replace("\'", "\\\'");
			str = str.Replace("\"", "\\\"");
			// 07/31/2006 Paul.  Stop using VisualBasic library to increase compatibility with Mono. 
			str = str.Replace("\t", "\\t");
			str = str.Replace("\r", "\\r");
			str = str.Replace("\n", "\\n");
			return str;
		}

		public static bool IsEmptyString(string str) {
			if (str == null || str == String.Empty)
				return true;
			return false;
		}

		public static bool IsEmptyString(object obj) {
			if (obj == null || obj == DBNull.Value)
				return true;
			if (obj.ToString() == String.Empty)
				return true;
			return false;
		}

		public static string ToString(string str) {
			if (str == null)
				return String.Empty;
			return str;
		}

		public static string ToString(object obj) {
			if (obj == null || obj == DBNull.Value)
				return String.Empty;
			return obj.ToString();
		}

		public static object ToDBString(string str) {
			if (str == null)
				return DBNull.Value;
			if (str == String.Empty)
				return DBNull.Value;
			return str;
		}

		public static object ToDBString(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			string str = obj.ToString();
			if (str == String.Empty)
				return DBNull.Value;
			return str;
		}

		public static byte[] ToBinary(object obj) {
			if (obj == null || obj == DBNull.Value)
				return new byte[0];
			return (byte[])obj;
		}

		public static object ToDBBinary(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			return obj;
		}

		public static object ToDBBinary(byte[] aby) {
			if (aby == null)
				return DBNull.Value;
			else if (aby.Length == 0)
				return DBNull.Value;
			return aby;
		}

		public static DateTime ToDateTime(DateTime dt) {
			return dt;
		}

		public static DateTime ToDateTime(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DateTime.MinValue;
			// If datatype is DateTime, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.DateTime"))
				return Convert.ToDateTime(obj);
			if (!Information.IsDate(obj))
				return DateTime.MinValue;
			return Convert.ToDateTime(obj);
		}

		// 05/27/2007 Paul.  It looks better to show nothing than to show 01/01/0001 12:00:00 AM. 
		public static string ToString(DateTime dt) {
			// If datatype is DateTime, then nothing else is necessary. 
			if (dt == DateTime.MinValue)
				return String.Empty;
			return dt.ToString();
		}

		public static string ToDateString(object obj) {
			if (obj == null || obj == DBNull.Value)
				return String.Empty;
			// If datatype is DateTime, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.DateTime"))
				return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
			if (!Information.IsDate(obj))
				return String.Empty;
			return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
		}

		public static string ToDateString(DateTime dt) {
			// If datatype is DateTime, then nothing else is necessary. 
			if (dt == DateTime.MinValue)
				return String.Empty;
			return dt.ToString("yyyy-MM-dd");
		}

		public static string ToDateString(DateTime dt, string format) {
			// If datatype is DateTime, then nothing else is necessary. 
			if (dt == DateTime.MinValue)
				return String.Empty;
			return dt.ToString(format);
		}

		public static string ToTimeString(DateTime dt) {
			// If datatype is DateTime, then nothing else is necessary. 
			if (dt == DateTime.MinValue)
				return String.Empty;
			return dt.ToShortTimeString();
		}

		public static object ToDBDateTime(DateTime dt) {
			if (dt == DateTime.MinValue)
				return DBNull.Value;
			return dt;
		}

		public static object ToDBDateTime(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			if (!Information.IsDate(obj))
				return DBNull.Value;
			DateTime dt = Convert.ToDateTime(obj);
			if (dt == DateTime.MinValue)
				return DBNull.Value;
			return dt;
		}

		public static bool IsEmptyGuid(Guid g) {
			if (g == Guid.Empty)
				return true;
			return false;
		}

		public static bool IsEmptyGuid(object obj) {
			if (obj == null || obj == DBNull.Value)
				return true;
			string str = obj.ToString();
			if (str == String.Empty)
				return true;
			Guid g = XmlConvert.ToGuid(str);
			if (g == Guid.Empty)
				return true;
			return false;
		}

		public static Guid ToGuid(Guid g) {
			return g;
		}

		public static Guid ToGuid(object obj) {
			if (obj == null || obj == DBNull.Value)
				return Guid.Empty;
			if (obj.GetType() == Type.GetType("System.Guid"))
				return (Guid)obj;
			string str = obj.ToString();
			if (str == String.Empty)
				return Guid.Empty;
			return XmlConvert.ToGuid(str);
		}

		public static object ToDBGuid(Guid g) {
			if (g == Guid.Empty)
				return DBNull.Value;
			return g;
		}

		public static object ToDBGuid(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			// If datatype is Guid, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Guid"))
				return obj;
			string str = obj.ToString();
			if (str == String.Empty)
				return DBNull.Value;
			Guid g = XmlConvert.ToGuid(str);
			if (g == Guid.Empty)
				return DBNull.Value;
			return g;
		}


		public static Int32 ToInteger(Int32 n) {
			return n;
		}

		public static Int32 ToInteger(object obj) {
			if (obj == null || obj == DBNull.Value)
				return 0;
			// If datatype is Integer, then nothing else is necessary. 
            if (obj.GetType() == Type.GetType("System.Int32"))
                return Convert.ToInt32(obj);
            else if (obj.GetType() == Type.GetType("System.Boolean"))
                return (Int32)(Convert.ToBoolean(obj) ? 1 : 0);
            else if (obj.GetType() == Type.GetType("System.Single"))
                return Convert.ToInt32(Math.Floor((System.Single)obj));
            else if (obj.GetType() == Type.GetType("System.Double"))
                return Convert.ToInt32(Math.Floor((System.Double)obj));
			string str = obj.ToString();
			if (str == String.Empty)
				return 0;
			return Int32.Parse(str, NumberStyles.Any);
		}
        public static Int32 ToInteger(object obj,Int32 defValue)
        {
            if (obj == null || obj == DBNull.Value)
                return defValue;
            // If datatype is Integer, then nothing else is necessary. 
            if (obj.GetType() == Type.GetType("System.Int32"))
                return Convert.ToInt32(obj);
            else if (obj.GetType() == Type.GetType("System.Boolean"))
                return (Int32)(Convert.ToBoolean(obj) ? 1 : 0);
            else if (obj.GetType() == Type.GetType("System.Single"))
                return Convert.ToInt32(Math.Floor((System.Single)obj));
            string str = obj.ToString();
            if (str == String.Empty)
                return defValue;
            Int32 v = defValue;            
            try
            {
                v = Int32.Parse(str);
            }
            catch
            {
                v = 0;
            }
            return defValue;
        }

		public static long ToLong(long n) {
			return n;
		}

		public static long ToLong(object obj) {
			if (obj == null || obj == DBNull.Value)
				return 0;
			// If datatype is Integer, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Int64"))
				return Convert.ToInt64(obj);
			string str = obj.ToString();
			if (str == String.Empty)
				return 0;
			return Int64.Parse(str, NumberStyles.Any);
		}

		public static short ToShort(short n) {
			return n;
		}

		public static short ToShort(int n) {
			return (short)n;
		}

		public static short ToShort(object obj) {
			if (obj == null || obj == DBNull.Value)
				return 0;
			// 12/02/2005 Paul.  Still need to convert object to Int16. Cast to short will not work. 
			if (obj.GetType() == Type.GetType("System.Int32") || obj.GetType() == Type.GetType("System.Int16"))
				return Convert.ToInt16(obj);
			string str = obj.ToString();
			if (str == String.Empty)
				return 0;
			return Int16.Parse(str, NumberStyles.Any);
		}

		public static object ToDBInteger(Int32 n) {
			return n;
		}

		public static object ToDBInteger(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			// If datatype is Integer, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Int32"))
				return obj;
			string str = obj.ToString();
			if (str == String.Empty || !Information.IsNumeric(str))
				return DBNull.Value;
			return Int32.Parse(str, NumberStyles.Any);
		}


		public static float ToFloat(float f) {
			return f;
		}

		public static float ToFloat(object obj) {
			if (obj == null || obj == DBNull.Value)
				return 0;
			// If datatype is Double, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Double"))
				return (float)Convert.ToSingle(obj);
			string str = obj.ToString();
			if (str == String.Empty || !Information.IsNumeric(str))
				return 0;
			return float.Parse(str, NumberStyles.Any);
		}

		public static float ToFloat(string str) {
			if (str == null)
				return 0;
			if (str == String.Empty || !Information.IsNumeric(str))
				return 0;
			return float.Parse(str, NumberStyles.Any);
		}

		public static object ToDBFloat(float f) {
			return f;
		}

		public static object ToDBFloat(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			// If datatype is Double, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Double"))
				return obj;
			string str = obj.ToString();
			if (str == String.Empty || !Information.IsNumeric(str))
				return DBNull.Value;
			return float.Parse(str, NumberStyles.Any);
		}


		public static double ToDouble(double d) {
			return d;
		}

		public static double ToDouble(object obj) {
			if (obj == null || obj == DBNull.Value)
				return 0;
			// If datatype is Double, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Double"))
				return Convert.ToDouble(obj);
			string str = obj.ToString();
			if (str == String.Empty || !Information.IsNumeric(str))
				return 0;
			return double.Parse(str, NumberStyles.Any);
		}

		public static double ToDouble(string str) {
			if (str == null)
				return 0;
			if (str == String.Empty || !Information.IsNumeric(str))
				return 0;
			return double.Parse(str, NumberStyles.Any);
		}


		public static Decimal ToDecimal(Decimal d) {
			return d;
		}

		public static Decimal ToDecimal(double d) {
			return Convert.ToDecimal(d);
		}

		public static Decimal ToDecimal(float f) {
			return Convert.ToDecimal(f);
		}

		public static Decimal ToDecimal(object obj) {
			if (obj == null || obj == DBNull.Value)
				return 0;
			// If datatype is Decimal, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Decimal"))
				return Convert.ToDecimal(obj);
			string str = obj.ToString();
			if (str == String.Empty)
				return 0;
			return Decimal.Parse(str, NumberStyles.Any);
		}

		public static object ToDBDecimal(Decimal d) {
			return d;
		}

		public static object ToDBDecimal(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			// If datatype is Decimal, then nothing else is necessary. 
			if (obj.GetType() == Type.GetType("System.Decimal"))
				return obj;
			string str = obj.ToString();
			if (str == String.Empty || !Information.IsNumeric(str))
				return DBNull.Value;
			return Decimal.Parse(str, NumberStyles.Any);
		}


		public static Boolean ToBoolean(Boolean b) {
			return b;
		}

		public static Boolean ToBoolean(Int32 n) {
			return (n == 0) ? false : true;
		}

		public static Boolean ToBoolean(object obj) {
			if (obj == null || obj == DBNull.Value)
				return false;
			if (obj.GetType() == Type.GetType("System.Int32"))
				return (Convert.ToInt32(obj) == 0) ? false : true;
			// 01/15/2007 Paul.  Allow a Byte field to also be treated as a boolean. 
			if (obj.GetType() == Type.GetType("System.Byte"))
				return (Convert.ToByte(obj) == 0) ? false : true;
			// 12/19/2005 Paul.  MySQL 5 returns SByte for a TinyInt. 
			if (obj.GetType() == Type.GetType("System.SByte"))
				return (Convert.ToSByte(obj) == 0) ? false : true;
			// 12/17/2005 Paul.  Oracle returns booleans as Int16. 
			if (obj.GetType() == Type.GetType("System.Int16"))
				return (Convert.ToInt16(obj) == 0) ? false : true;
			// 03/06/2006 Paul.  Oracle returns SYNC_CONTACT as decimal.
			if (obj.GetType() == Type.GetType("System.Decimal"))
				return (Convert.ToDecimal(obj) == 0) ? false : true;
			if (obj.GetType() == Type.GetType("System.String")) {
				string s = obj.ToString().ToLower();
				return (s == "true" || s == "on" || s == "1") ? true : false;
			}
			if (obj.GetType() != Type.GetType("System.Boolean"))
				return false;
			return bool.Parse(obj.ToString());
		}

		public static object ToDBBoolean(Boolean b) {
			// 03/22/2006 Paul.  DB2 requires that we convert the boolean to an integer.  It makes sense to do so for all platforms. 
			return b ? 1 : 0;
		}

		public static object ToDBBoolean(object obj) {
			if (obj == null || obj == DBNull.Value)
				return DBNull.Value;
			if (obj.GetType() != Type.GetType("System.Boolean")) {
				// 10/01/2006 Paul.  Return 0 instead of false, as false can be converted to text. 
				string s = obj.ToString().ToLower();
				return (s == "true" || s == "on" || s == "1") ? 1 : 0;
			}
			// 03/22/2006 Paul.  DB2 requires that we convert the boolean to an integer.  It makes sense to do so for all platforms. 
			return Convert.ToBoolean(obj) ? 1 : 0;
		}

		public static object ToChineseSerial(int serial) {
			string str = string.Empty;
			switch (serial) {
				case 0: str = "零"; break;
				case 1: str = "一"; break;
				case 2: str = "二"; break;
				case 3: str = "三"; break;
				case 4: str = "四"; break;
				case 5: str = "五"; break;
				case 6: str = "六"; break;
				case 7: str = "七"; break;
				case 8: str = "八"; break;
				case 9: str = "九"; break;
				default: str = ""; break;
			}

			return str;
		}


        /// <summary>
        /// url查询参数过滤
        /// </summary>
        /// <param name="pama"></param>
        /// <returns></returns>
        public static  string urlPamaFilter(string pama)
        {
            if (string.IsNullOrEmpty(pama))
            {
                return "";
            }
            pama = pama.Replace("'", "＇");
            pama = pama.Replace("\"", " ");
            pama = pama.Replace("&", "&amp");
            pama = pama.Replace("<", "&lt");
            pama = pama.Replace(">", "&gt");       
            pama = pama.Replace("--", "－－");          
            pama = pama.Replace(";", "；");
            pama = pama.Replace("(", "（");
            pama = pama.Replace(")", "）");
            pama = pama.Replace("0x", "0 x");    //防止16进制注入
           
            pama = pama.Replace("Exec", "");
            pama = pama.Replace("Execute", "");
  
            pama = pama.Replace("xp_", "x p_");
            pama = pama.Replace("sp_", "s p_");
            return pama;
        }

	}

    public class Information
    {
        public static bool IsDate(object o)
        {
            if (o == null)
                return false;
            else if (o is DateTime)
                return true;
            else if (o is String)
            {
                try
                {
                    DateTime.Parse(o as String);
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }

        public static bool IsNumeric(object o)
        {
            if (o == null || o is DateTime)
                return false;
            else if (o is Int16 || o is Int32 || o is Int64 || o is Decimal || o is Single || o is Double)
                return true;
            else
            {
                try
                {
                    if (o is String)
                        Double.Parse(o as String);
                    else
                        Double.Parse(o.ToString());
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }
    }
}
