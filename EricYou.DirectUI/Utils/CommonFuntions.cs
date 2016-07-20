using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel.Design;
using EnvDTE;
using System.IO;
using EricYou.DirectUI.Native;
//using EnvDTE;
//using EnvDTE100;
//using System.Reflection;

namespace EricYou.DirectUI.Utils
{
    /// <summary>
    /// 通用方法类
    /// </summary>
    public class CommonFunctions
    {
        /// <summary>
        /// 判断当前代码是否在设计时状态下执行
        /// </summary>
        /// <returns>在设计时状态则返回true</returns>
        public static bool IsInDesignMode()
        {
            if (System.ComponentModel.LicenseManager.UsageMode
                == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                //MessageBox.Show("Design Time!");
                return true;
            }
            else
            {
                //MessageBox.Show("RunTime Time!");
                return false;
            }
        }

        /// <summary>
        /// 返回当前程序根目录
        /// </summary>
        /// <returns></returns>
        public static string GetAppBaseDirectory()
        {
            if (CommonFunctions.IsInDesignMode())
            {
                //方案1：
                //return @"F:\技术资料\斌哥App\场地管理\2012-09-27\SportsVenues\UIDemo\bin\Debug";
                //return @"F:\技术资料\斌哥App\场地管理\2012-12-03\.Venues\UI\bin\Debug";
                //return @"F:\private\创\预定\code\.Venues\UI\bin\Debug";

                //方案2
                //ITypeResolutionService typeResService
                //    = GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
                //string path = typeResService.GetPathOfAssembly(Assembly.GetExecutingAssembly().GetName());
                //MessageBox.Show(path);

                //方案3：可获得解决方案路径
                //return Environment.CurrentDirectory;

                //方案4：
                //string solutionDirectory = ((EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0")).ActiveDocument.ProjectItem.ContainingProject.FullName;
                //MessageBox.Show(solutionDirectory);
                //solutionDirectory = System.IO.Path.GetDirectoryName(solutionDirectory) + "\\bin\\Debug";
                //MessageBox.Show(solutionDirectory);

                //方案5：正确的方法
                //DTE dte = (EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.14.0");
                DTE dte = GetDTEObject();
                string solutionDirectory = GetActiveProject(dte).FullName;
                solutionDirectory = System.IO.Path.GetDirectoryName(solutionDirectory);// + "\\bin\\Debug";
                //MessageBox.Show(solutionDirectory);
                return solutionDirectory;


            }
            else
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }

        //获取当前版本的visual studio实例（版本从10.0开始尝试往上加）
        internal static EnvDTE.DTE GetDTEObject()
        {
            DTE dteObject = null;
            for(int version=10;version<=40;version++)
            {
                string vsVersionString = string.Format("VisualStudio.DTE.{0}.0", version);
                try
                {
                    dteObject = (EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject(vsVersionString);
                    if(dteObject != null)
                    {
                        return dteObject;
                    }
                }
                catch
                {
                    continue;
                }
            }
            return dteObject;
        }

        internal static Project GetActiveProject(DTE dte)
        {
            Project activeProject = null;
            Array activeSolutionProjects = dte.ActiveSolutionProjects as Array;
            if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
            {
                activeProject = activeSolutionProjects.GetValue(0) as Project;
            }
            return activeProject;
        } 

        /// <summary>
        /// 获取逻辑坐标点对应实际图片上的实际点坐标
        /// 逻辑坐标点横纵坐标为负数表示距离窗口右或下边界的距离，不是实际坐标）
        /// </summary>
        /// <param name="sourceImage">图片对象</param>
        /// <param name="p">逻辑坐标点</param>
        /// <returns>实际图片坐标点</returns>
        public static Point GetImagePoint(Image sourceImage, int offsetX, int offsetY, Point p)
        {
            Point newPoint = new Point(p.X, p.Y);

            //负数表示使用实际图片宽度减掉当前值
            if (newPoint.X < 0)
            {
                newPoint.X = sourceImage.Width +newPoint.X;// +offsetX;
            }
            //负数表示使用实际图片高度减掉当前值
            if (newPoint.Y < 0)
            {
                newPoint.Y = sourceImage.Height + newPoint.Y;// +offsetY;
            }
            return newPoint;
        }

        /// <summary>
        /// 从指定路径和文件名加载图片文件并以Image对象返回
        /// </summary>
        /// <param name="imageFileName">图片路径和文件名</param>
        /// <returns>Image对象</returns>
        public static Image LoadImageFromFile(string imageFileName)
        {
            Image retImage = null;
            if (!File.Exists(imageFileName))
            {
                throw new Exception("加载图片资源时找不到文件" + imageFileName + "!");
            }

            using (Stream stream = File.Open(imageFileName, FileMode.Open))
            {
                retImage = Image.FromStream(stream);
            }
            return retImage;
        }

        /// <summary>
        /// 返回设计时窗口采用的主题的唯一名称
        /// </summary>
        /// <returns></returns>
        public static string GetDesignModeSkinName()
        {
            return "Default";
        }

        /// <summary>
        /// 调用Win32 BitBlt函数拷贝图像
        /// </summary>
        /// <param name="desGraphics">目标Graphics对象</param>
        /// <param name="desX">目标贴图起始横坐标</param>
        /// <param name="dexY">目标贴图起始纵坐标</param>
        /// <param name="desWidth">目标贴图宽度</param>
        /// <param name="desHeight">目标贴图高度</param>
        /// <param name="srcImage">贴图源Image对象</param>
        /// <param name="srcX">贴图源起始横坐标</param>
        /// <param name="srcY">贴图源起始纵坐标</param>
        public static void BitBltDrawImage(Graphics desGraphics, int desX, int dexY, int desWidth, int desHeight,
            Bitmap srcImage, int srcX, int srcY)
        {
            using (Graphics srcGraphics = Graphics.FromImage(srcImage))
            {
                IntPtr hdcSrc = srcGraphics.GetHdc();
                IntPtr hBitmapSrc = srcImage.GetHbitmap();
                IntPtr cdcSrc = NativeMethods.CreateCompatibleDC(hdcSrc);
                IntPtr oldObject = NativeMethods.SelectObject(cdcSrc, hBitmapSrc);

                IntPtr hdcDes = desGraphics.GetHdc();

                bool result = NativeMethods.BitBlt(
                    hdcDes, desX, dexY, desWidth, desHeight, cdcSrc, srcX, srcY, NativeConst.ROP_SRCCOPY);

                NativeMethods.SelectObject(cdcSrc,oldObject);
                NativeMethods.DeleteObject(hBitmapSrc);
                NativeMethods.DeleteDC(cdcSrc);

                srcGraphics.ReleaseHdc(hdcSrc);
                desGraphics.ReleaseHdc(hdcDes);
            }
        }

        /// <summary>
        /// 设置指定hWnd窗口句柄对应控件的Window Style或者Extended Window Styles
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="nIndex">偏移量，取GWL_和DWL_开头的常量</param>
        /// <param name="windowStyle">窗口样式常量，取WS_和WS_EX_开头的常量</param>
        public static void SetWindowStyle(IntPtr hWnd, int nIndex, long windowStyle)
        {
            long WindowLongValue = NativeMethods.GetWindowLong(hWnd, nIndex);
            WindowLongValue |= windowStyle;

            NativeMethods.SetWindowLong(hWnd, nIndex, WindowLongValue);
        }

        /// <summary>
        /// 取消设置指定hWnd窗口句柄对应控件的Window Style或者Extended Window Styles
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="nIndex">偏移量，取GWL_和DWL_开头的常量</param>
        /// <param name="windowStyle">窗口样式常量，取WS_和WS_EX_开头的常量</param>
        public static void UnsetWindowStyle(IntPtr hWnd, int nIndex, long windowStyle)
        {
            long WindowLongValue = NativeMethods.GetWindowLong(hWnd, nIndex);
            WindowLongValue &= ~windowStyle;

            NativeMethods.SetWindowLong(hWnd, nIndex, WindowLongValue);
        }

        private static int iCaptionHeight = NativeMethods.GetSystemMetrics(NativeConst.SM_CYCAPTION);  //标题栏高度
        private static int iCYFrame = NativeMethods.GetSystemMetrics(NativeConst.SM_CYFRAME);          //可变大小的窗口的上下边框的厚度
        private static int iCXFrame = NativeMethods.GetSystemMetrics(NativeConst.SM_CYFRAME);          //可变大小的窗口的左右边框的厚度
        private static int iFixedCYFrame = NativeMethods.GetSystemMetrics(NativeConst.SM_CYFIXEDFRAME);//不可变大小的窗口的上下边框的厚度
        private static int iFixedCXFrame = NativeMethods.GetSystemMetrics(NativeConst.SM_CXFIXEDFRAME);//不可变大小的窗口的左右边框的厚度

        /// <summary>
        /// 获取指定边框类型的DUIFrom窗口Resize后,窗口大小需要调整的偏移量
        /// </summary>
        /// <param name="boderStyle">窗口边框类型</param>
        /// <returns>偏移量对象，包含Width偏移量和Height偏移量</returns>
        public static Size GetWindowResizeOffset(FormBorderStyle boderStyle)
        {
            Size offsetSize = new Size(0, 0);

            //当前窗口为不可变窗口时
            if (boderStyle == System.Windows.Forms.FormBorderStyle.Fixed3D
                || boderStyle == System.Windows.Forms.FormBorderStyle.FixedDialog
                || boderStyle == System.Windows.Forms.FormBorderStyle.FixedSingle
                || boderStyle == System.Windows.Forms.FormBorderStyle.FixedToolWindow)
            {
                offsetSize.Width = 2 * CommonFunctions.iFixedCXFrame;
                offsetSize.Height = (2 * CommonFunctions.iFixedCYFrame + CommonFunctions.iCaptionHeight);
            }
            //当前窗口为可变窗口时
            else if (boderStyle == System.Windows.Forms.FormBorderStyle.Sizable
                || boderStyle == System.Windows.Forms.FormBorderStyle.SizableToolWindow)
            {
                offsetSize.Width = 2 * CommonFunctions.iCXFrame;
                offsetSize.Height = (2 * CommonFunctions.iCYFrame + CommonFunctions.iCaptionHeight);
            }
            //当前窗口无边框
            else { ;}

            return offsetSize;
        }

        /// <summary>
        /// 窗口截图，包含标题栏和内容区
        /// </summary>
        /// <param name="window">窗口对象</param>
        /// <returns>截图Bitmap对象</returns>
        public static Bitmap CaptureWindow(System.Windows.Forms.Form window)
        {
            Rectangle rc = new Rectangle(window.Location, window.Size);
            Bitmap memoryImage = null;

            try
            {
                // Create new graphics object using handle to window.
                using (Graphics graphics = window.CreateGraphics())
                {
                    memoryImage = new Bitmap(rc.Width, rc.Height, graphics);

                    using (Graphics memoryGrahics = Graphics.FromImage(memoryImage))
                    {
                        memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Capture failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return memoryImage;
        }
        
       /*const int SRCCOPY = 0x00CC0020;
        public bool CutPictrueToStream(Bitmap BmpSource)
        {
            Graphics grSource = Graphics.FromImage(BmpSource);
            Bitmap Bitmap_cutted = new Bitmap(SizeX, SizeY, grSource);
            IntPtr hdcTarget = IntPtr.Zero;
            IntPtr hdcSource = IntPtr.Zero;
            IntPtr hBitmapSource = IntPtr.Zero;
            IntPtr hOldObject = IntPtr.Zero;
            hBitmapSource = BmpSource.GetHbitmap();
            MemorySource = new MemoryStream[ClassCount][][];
            for (int i = 0; i < ClassCount; i++)
            {
                MemorySource[i] = new MemoryStream[DirectionCount][];
                for (int j = 0; j < DirectionCount; j++)
                {
                    MemorySource[i][j] = new MemoryStream[Frames[i]];
                    for (int k = 0; k < Frames[i]; k++)
                    {
                        Graphics grTarget = Graphics.FromImage(Bitmap_cutted);
                        hdcTarget = grTarget.GetHdc();
                        hdcSource = grSource.GetHdc();
                        hOldObject = SelectObject(hdcSource, hBitmapSource);
                        BitBlt(hdcTarget, 0, 0, SizeX, SizeY, hdcSource, (i * Frames[i] + k) * SizeX, j * SizeY, SRCCOPY);
                        //必须释放DC，否则保存为黑图
                        if (hdcTarget != IntPtr.Zero
                        {
                            grTarget.ReleaseHdc(hdcTarget);
                        }
                        if (hdcSource != IntPtr.Zero)
                        {
                            grSource.ReleaseHdc(hdcSource);
                        }
                        Bitmap_cutted.MakeTransparent();//保存为透明背景
                        Bitmap_cutted.Save(@"F:\Project\VS 2008\C#\(十一)地图遮罩层的实现\(十一)地图遮罩层的实现\Player\" + i.ToString() + "_" + j.ToString() + "_" + k.ToString() + ".Png",ImageFormat.Png);
                        //
                        MemorySource[i][j][k] = new MemoryStream();
                        //
                        Bitmap_cutted.Save(MemorySource[i][j][k], ImageFormat.Png);
                        grTarget.Dispose();
                    }
                }
            }
            if (hOldObject != IntPtr.Zero)
                SelectObject(hdcSource, hOldObject);
            if (hBitmapSource != IntPtr.Zero)
                DeleteObject(hBitmapSource);
            grSource.Dispose();
            Bitmap_cutted.Dispose();
            return true;
        }*/
    }

}
