using System;
using System.Text;
using System.IO;
namespace U8DataAccess
{
	
	public class LogNote
	{
		public static string cUser_Name="" ;

		string filename="RunNote.log";
		System.IO.BinaryWriter filestream=null;
		public LogNote()
		{
			if(!File.Exists(this.filename))							//����ļ��Ƿ����
			{
				
				filestream=new BinaryWriter( new FileStream(this.filename,FileMode.Create,FileAccess.Write,FileShare.ReadWrite),
					System.Text.Encoding.Default);						
			}
			else
			{
				filestream=new BinaryWriter( new FileStream(this.filename,FileMode.Append,FileAccess.Write,FileShare.ReadWrite),
					System.Text.Encoding.Default);			
			}
		}
		public LogNote(string filename)
		{
			
			this.filename=filename;
			if(!File.Exists(this.filename))							//����ļ��Ƿ����
			{
				filestream=new BinaryWriter( new FileStream(this.filename,FileMode.Create,FileAccess.Write,FileShare.ReadWrite),
					System.Text.Encoding.Default);						
			}
			else
			{
				filestream=new BinaryWriter( new FileStream(this.filename,FileMode.Append,FileAccess.Write,FileShare.ReadWrite),
					System.Text.Encoding.Default);			
			}
		}
		/*
		 ********************************************************************** 
		 * ����:��¼����
		 * ����:action ����, data��¼����, datetime ʱ��
		 ********************************************************************** 
		 */ 
		public bool Write(string action,string data,string datetime)
		{
			string writetemp=" " + datetime;
			try
			{
				writetemp =writetemp + action.ToString() + "----" + data.ToString() + ((char)0x0d).ToString() +((char)0x0a).ToString() ;
				filestream.Write(writetemp);
				filestream.Flush();
				return true;
			}
			catch
			{
				return false;
			}
			//return false;
		}
		/*
		 *********************************************************************** 
		 * ����:��¼����
		 * ����:action ����, data��¼����
		 *********************************************************************** 
		 */ 
		public bool Write(string action,string data)
		{
			return Write(action,data,DateTime.Now.Year + "/" + DateTime.Now.Month + "/" +DateTime.Now.Day + "  " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + " �� ");
		}
//		~LogNote()
//		{
//			filestream.Close();
//		}
	}
}
