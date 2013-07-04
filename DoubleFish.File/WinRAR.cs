using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace DoubleFish.File
{
	public class WinRAR
	{
		public WinRAR ()
		{
			//判断是否安装了WinRAR.exe
			RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
			_Installed = !string.IsNullOrEmpty(key.GetValue(string.Empty).ToString());

			//获取WinRAR.exe路径
			_ApplicationPath = key.GetValue(string.Empty).ToString();
		}

		/// <summary>
		/// 是否已安装WinRAR.exe
		/// </summary>
		private bool _Installed;
		/// <summary>
		/// 获取是否安装了WinRAR的标识
		/// </summary>
		public bool Installed
		{
			get { return _Installed; }
		}

		private string _ApplicationPath;
		/// <summary>
		/// 获取WinRAR.exe路径
		/// </summary>
		public string ApplicationPath
		{
			get { return _ApplicationPath; }
		}

		private int _Level;
		/// <summary>
		/// 设置压缩级别（0-5）
		/// </summary>
		public int Level
		{
			set { _Level = value; }
			get
			{
				if (_Level > 5)
					return 5;
				else if (_Level < 0)
					return 0;
				else
					return _Level;
			}
		}

		/// <summary>
		/// 利用WinRAR进行压缩
		/// </summary>
		/// <param name="sourcePath">要压缩的文件夹（绝对路径）</param>
		/// <param name="rarPath">压缩后的.rar文件的存放目录（绝对路径）</param>
		/// <param name="rarName">压缩文件的名称（包括后缀）</param>
		public void Compress (string sourcePath, string rarPath, string rarName)
		{
			try
			{
				if (!Installed)
				{
					//throw new ArgumentException("Not setuping the winRar, you can Compress.make sure setuped winRar.");
					throw new ArgumentException("未安装WinRAR，请在确定安装WinRAR后再进行压缩操作！");
				}
				//判断输入目录是否存在
				if (!Directory.Exists(sourcePath))
				{
					//throw new ArgumentException("CompressRar'arge : inputPath isn't exsit.");
					throw new ArgumentException("要压缩的文件目录不存在！");
				}
				//rar 执行时的命令、参数
				//命令参数
				//rarCmd = " a -m0 " + rarName + " " + sourcePath + " *.* -r";
				string cmd = string.Format("a -m{0} -ep1 \"{1}\" \"{2}\" -r", this.Level, rarName, sourcePath);

				//创建启动进程的参数
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				//指定启动文件名
				processStartInfo.FileName = ApplicationPath;
				//指定启动该文件时的命令、参数
				processStartInfo.Arguments = cmd;
				//指定启动窗口模式：隐藏
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				//指定压缩后到达路径
				processStartInfo.WorkingDirectory = rarPath;
				//创建进程对象
				Process process = new Process();
				//指定进程对象启动信息对象
				process.StartInfo = processStartInfo;
				//启动进程
				process.Start();
				//指定进程自行退行为止
				process.WaitForExit();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 利用WinRAR进行解压缩
		/// </summary>
		/// <param name=”path”>文件解压路径（绝对）</param>
		/// <param name="rarName">要解压缩的.rar文件名（包括后缀）</param>
		/// <param name="rarPath">要解压缩的.rar文件的存放目录（绝对路径）</param>
		public void UnCompress (string path, string rarName, string rarPath)
		{
			//rar 执行时的命令、参数
			string cmd;
			//启动进程的参数
			ProcessStartInfo processStartInfo;
			//进程对象
			Process process;
			try
			{
				if (!Installed)
				{
					throw new ArgumentException("Not setuping the winRar, you can UnCompress.make sure setuped winRar.");
				}
				//如果压缩到目标路径不存在
				if (!Directory.Exists(path))
				{
					//创建压缩到目标路径
					Directory.CreateDirectory(path);
				}
				//cmd = "x " + rarName + " " + path + " -y";
				cmd = string.Format("x \"{0}\" \"{1}\" -y", rarName, path);

				processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = ApplicationPath;
				processStartInfo.Arguments = cmd;
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				processStartInfo.WorkingDirectory = rarPath;

				process = new Process();
				process.StartInfo = processStartInfo;
				process.Start();
				process.WaitForExit();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
