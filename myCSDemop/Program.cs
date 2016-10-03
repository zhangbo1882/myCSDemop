using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace myCSDemop
{
    class Log
    {
        public static FileStream fs;
        public static StreamWriter sw;
        public Log (string logFilePath, string logFileName)
        {
            string fileName;
            if (!Directory.Exists(logFilePath))//验证路径是否存在
            {
                Console.Write("path does not exist\n");
                Directory.CreateDirectory(logFilePath);
                //不存在则创建
            }
            fileName = logFilePath + "\\" + logFileName; //文件的绝对路径
            //fileName = logFileName; //文件的绝对路径
            if (File.Exists(fileName))
            //验证文件是否存在，有则追加，无则创建
            {
                Console.Write("file already exists\n");
                fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                Console.Write("file does not exist\n");
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
        }

        public void WriteLog(string logMsg, params object[] list)
        {
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + logMsg, list);
            sw.Flush();
        }
    };

    //! Application信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIApplicationInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 513)]
        public byte [] AuthCode;                              ///< 授权码
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 301)]
        public byte [] KeyOperationLogPath;                    ///< 关键操作日志路径
    };


    //! 品种编码结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPICommodity
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte [] ExchangeNo;                             ///< 交易所编码
        public byte CommodityType;                          ///< 品种类型
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte [] CommodityNo;                            ///< 品种编号
    };

    struct APICommodity
    {
        public string ExchangeNo;
        public char CommodityType;
        public string CommodityNo;
    };

    //! 合约编码结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIContract
    {
        public TapAPICommodity Commodity;                              ///< 品种
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] ContractNo1;                            ///< 合约代码1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte [] StrikePrice1;                           ///< 执行价1
        public byte CallOrPutFlag1;                         ///< 看涨看跌标示1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] ContractNo2;                            ///< 合约代码2
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] StrikePrice2;                           ///< 执行价2
        public byte CallOrPutFlag2;                         ///< 看涨看跌标示2
    };

    struct APIContract
    {
        public APICommodity Commodity;
        public string ContractNo1;
        public string StrikePrice1;
        public char CallOrPutFlag1;
        public string ContractNo2;
        public string StrikePrice2;
        public char CallOrPutFlag2;
    };


    //! 交易所信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIExchangeInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] ExchangeNo;                              ///< 交易所编码
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        byte [] ExchangeName;                            ///< 交易所名称
    };

    //! 修改密码请求
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIChangePasswordReq
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        byte [] OldPassword;                         ///< 旧密码
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        byte [] NewPassword;                         ///< 新密码
    };

    //! 登录认证信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIQuoteLoginAuth
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte [] UserNo;                  ///< 用户名
        public byte ISModifyPassword;        ///< 是否修改密码，'Y'表示是，'N'表示否
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte [] Password;                ///< 用户密码
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte [] NewPassword;         ///< 新密码，如果设置了修改密码则需要填写此字段
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte [] QuoteTempPassword;       ///< 行情临时密码
        public byte ISDDA;                   ///< 是否需呀动态认证，'Y'表示是，'N'表示否
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
        public byte [] DDASerialNo;         ///< 动态认证码
    };

    struct APIQuoteLoginAuth
    {
        public string UserNo;
        public char ISModifyPassword;
        public string Password;
        public string NewPassword;
        public string QuoteTempPassword;
        public char ISDDA;
        public string DDASerialNo;      
    }

    //! 登录反馈信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIQuotLoginRspInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte [] UserNo;                          ///< 用户名
        public int UserType;                     ///< 用户类型
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte[] UserName;                        ///< 昵称，GBK编码格式
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte[] QuoteTempPassword;               ///< 行情临时密码
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)]
        public byte[] ReservedInfo;                    ///< 用户自己设置的预留信息
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
        public byte[] LastLoginIP;                 ///< 上次登录的地址
        public UInt32 LastLoginPort;                   ///< 上次登录使用的端口
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] LastLoginTime;                 ///< 上次登录的时间
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] LastLogoutTime;                    ///< 上次退出的时间
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] TradeDate;                     ///< 当前交易日期
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] LastSettleTime;                    ///< 上次结算时间
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] StartTime;                     ///< 系统启动时间
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] InitTime;                      ///< 系统初始化时间
    };

    struct APIQuoteLoginRspInfo
    {
        public string UserNo;
        public Int32 UserType;
        public string UserName;
        public string QuoteTempPassword;
        public string ReserveredInfo;
        public string LastLoginIP;
        public UInt32 LastLoginPort;
        public string LastLoginTime;
        public string LastLogoutTime;
        public string TradeDate;
        public string LastSettleTime;
        public string StartTime;
        public string InitTime;
    };


    //! 品种信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIQuoteCommodityInfo
    {
        public TapAPICommodity Commodity;                          ///< 品种
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public byte [] CommodityName;                       ///< 品种名称,GBK编码格式
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
        public byte [] CommodityEngName;                    ///< 品种英文名称
        public double ContractSize;                        ///< 每手乘数
        public double CommodityTickSize;                   ///< 最小变动价位
        public Int32 CommodityDenominator;             ///< 报价分母
        public byte CmbDirect;                         ///< 组合方向
        public Int32 CommodityContractLen;             ///< 品种合约年限
        public byte IsDST;                               ///< 是否夏令时,'Y'为是,'N'为否
        public TapAPICommodity RelateCommodity1;                   ///< 关联品种1
        public TapAPICommodity RelateCommodity2;                   ///< 关联品种2
    };

    struct APIQuoteCommodityInfo
    {
        public APICommodity Commodity;
        public string CommodityName;
        public string CommodityEngName;
        public double ContractSize;
        public double CommodityTickSize;
        public Int32 CommodityDenominator;
        public char CmbDirect;
        public Int32 CommodityContractLen;
        public char IsDST;
        public APICommodity RelateCommodity1;
        public APICommodity RelateCommodity2;  
    }

    //! 行情合约信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIQuoteContractInfo
    {
        TapAPIContract Contract;                            ///< 合约
        byte ContractType;                       ///< 合约类型,'1'表示交易行情合约,'2'表示行情合约
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] QuoteUnderlyingContract;         ///< 行情真实合约
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 71)]
        byte [] ContractName;                       ///< 合约名称
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] ContractExpDate;                    ///< 合约到期日	
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] LastTradeDate;                      ///< 最后交易日
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] FirstNoticeDate;                    ///< 首次通知日
    };

    //! 行情全文
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIQuoteWhole
    {
        public TapAPIContract Contract;                        ///< 合约
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] CurrencyNo;                      ///< 币种编号
        public byte TradingState;                  ///< 交易状态。1,集合竞价;2,集合竞价撮合;3,连续交易;4,交易暂停;5,闭市
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public byte[] DateTimeStamp;                  ///< 时间戳
        public double QPreClosingPrice;                ///< 昨收盘价
        public double QPreSettlePrice;             ///< 昨结算价
        public UInt64 QPrePositionQty;                ///< 昨持仓量
        public double QOpeningPrice;                   ///< 开盘价
        public double QLastPrice;                      ///< 最新价
        public double QHighPrice;                      ///< 最高价
        public double QLowPrice;                       ///< 最低价
        public double QHisHighPrice;                   ///< 历史最高价
        public double QHisLowPrice;                    ///< 历史最低价
        public double QLimitUpPrice;                   ///< 涨停价
        public double QLimitDownPrice;             ///< 跌停价
        public UInt64 QTotalQty;                      ///< 当日总成交量
        public double QTotalTurnover;                  ///< 当日成交金额
        public UInt64 QPositionQty;                   ///< 持仓量
        public double QAveragePrice;                   ///< 均价
        public double QClosingPrice;                   ///< 收盘价
        public double QSettlePrice;                    ///< 结算价
        public UInt64 QLastQty;                       ///< 最新成交量
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public double[] QBidPrice;                   ///< 买价1-20档
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public UInt64[] QBidQty;                    ///< 买量1-20档
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public double[] QAskPrice;                   ///< 卖价1-20档
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public UInt64[] QAskQty;                    ///< 卖量1-20档
        public double QImpliedBidPrice;                ///< 隐含买价
        public UInt64 QImpliedBidQty;                 ///< 隐含买量
        public double QImpliedAskPrice;                ///< 隐含卖价
        public UInt64 QImpliedAskQty;                 ///< 隐含卖量
        public double QPreDelta;                       ///< 昨虚实度
        public double QCurrDelta;                      ///< 今虚实度
        public UInt64 QInsideQty;                     ///< 内盘量
        public UInt64 QOutsideQty;                    ///< 外盘量
        public double QTurnoverRate;                   ///< 换手率
        public UInt64 Q5DAvgQty;                      ///< 五日均量
        public double QPERatio;                        ///< 市盈率
        public double QTotalValue;                 ///< 总市值
        public double QNegotiableValue;                ///< 流通市值
        public Int64 QPositionTrend;                   ///< 持仓走势
        public double QChangeSpeed;                    ///< 涨速
        public double QChangeRate;                 ///< 涨幅
        public double QChangeValue;                    ///< 涨跌值
        public double QSwing;                          ///< 振幅
        public UInt64 QTotalBidQty;                   ///< 委买总量
        public UInt64 QTotalAskQty;                   ///< 委卖总量
    };

    class QuoteAPI
    {
        private IntPtr quotePtr;
        private IntPtr quoteNotifyPtr;
        private string apiVersion;

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetQuoteAPIVersion();

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateQuoteAPI(IntPtr applicationInfo, ref Int32 iResult);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeQuoteAPI(IntPtr apiObj);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateNotifyAPI();

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAPINotify(IntPtr apiObj, IntPtr apiNotify);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetQuoteAPIDataPath([MarshalAs(UnmanagedType.LPStr)] string path);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetQuoteAPILogLevel(byte logLevel);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]

        public static extern int SetQuoteHostAddress(IntPtr apiObj, string ip, ushort port);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoginQuote(IntPtr apiObj,  IntPtr  loginAuth);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DisconnectQuote(IntPtr apiObj);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int QryCommodityQuote(IntPtr apiObj, out UInt32 sessionID);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int QryContractQuote(IntPtr apiObj, ref UInt32 sessionID, IntPtr qryReq);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Subscribe(IntPtr apiObj, ref UInt32 sessionID, IntPtr contract);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int UnSubscribe(IntPtr apiObj, ref UInt32 sessionID, IntPtr contract);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAPIReadyCB(IntPtr notifyObj, CallbackDelegateApiReady callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDisconnectCB(IntPtr notifyObj, CallbackDelegateDisconnect callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRspLoginCB(IntPtr notifyObj, CallbackDelegateRspLogin callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRspQryCommodityCB(IntPtr notifyObj, CallbackDelegateRspQryCommodity callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRspQryContractCB(IntPtr notifyObj, CallbackDelegateRspQryContract callback);


        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRspSubscribeQuoteCB(IntPtr notifyObj, CallbackDelegateRspSubscribeQuote callback);


        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRspUnSubscribeQuoteCB(IntPtr notifyObj, CallbackDelegateRspUnSubscribeQuote callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRtnQuoteCB(IntPtr notifyObj, CallbackDelegateRtnQuote callback);


        public delegate void CallbackDelegateApiReady();

        public delegate void CallbackDelegateDisconnect(int errCode);

        public delegate void CallbackDelegateRspLogin(int errCode, ref TapAPIQuotLoginRspInfo info);

        public delegate void CallbackDelegateRspQryCommodity(UInt32 sessionID, int errCode, byte isLast, ref TapAPIQuoteCommodityInfo info);

        public delegate void CallbackDelegateRspQryContract(UInt32 sessionID, int errCode, byte isLast, ref TapAPIQuoteContractInfo info);

        public delegate void CallbackDelegateRspSubscribeQuote(UInt32 sessionID, int errCode, byte isLast, ref TapAPIQuoteWhole info);

        public delegate void CallbackDelegateRspUnSubscribeQuote(UInt32 sessionID, int errCode, byte isLast, ref TapAPIContract info);

        public delegate void CallbackDelegateRtnQuote(ref TapAPIQuoteWhole info);
        /*The following functions are callback implementated by ourselves*/

        private static void apiReadyCB()
        {
            Console.Write("Call Back apiReadyCB\n");
        }

        private static void disconnectCB(int errCode)
        {
            Console.Write("CallBack disconnectCB: {0}\n", errCode);
        }

        private static void rspLoginCB(int errCode,  ref TapAPIQuotLoginRspInfo info)
        {
            APIQuoteLoginRspInfo data;
            Console.Write("CallBack rspLoginCB\n");
            Console.Write("ErrCode: {0}\n", errCode);

            data.UserNo = Encoding.Default.GetString(info.UserNo);
            data.UserType = info.UserType;
            data.UserName = Encoding.Default.GetString(info.UserName);
            data.QuoteTempPassword = Encoding.Default.GetString(info.QuoteTempPassword);
            data.ReserveredInfo = Encoding.Default.GetString(info.ReservedInfo);
            data.LastLoginIP = Encoding.Default.GetString(info.LastLoginIP);
            data.LastLoginPort = info.LastLoginPort;
            data.LastLoginTime = Encoding.Default.GetString(info.LastLoginTime);
            data.LastLogoutTime = Encoding.Default.GetString(info.LastLogoutTime);
            data.LastSettleTime = Encoding.Default.GetString(info.LastSettleTime);
            data.StartTime = Encoding.Default.GetString(info.StartTime);
            data.InitTime = Encoding.Default.GetString(info.InitTime);
        }

        public static void dumpCommodity(APICommodity com)
        {
            Console.WriteLine("CommodityNo: {0}", com.CommodityNo);
            Console.WriteLine("CommodityType: {0}", com.CommodityType);
            Console.WriteLine("ExchangeNo: {0}", com.ExchangeNo);
        }

        public static void dumpComInfo(APIQuoteCommodityInfo info)
        {
            dumpCommodity(info.Commodity);
            Console.WriteLine("CommodityName: {0}", info.CommodityName);
            Console.WriteLine("CommodityEngName: {0}", info.CommodityEngName);
            Console.WriteLine("ContractSize: {0}", info.ContractSize);
            Console.WriteLine("CommodityTickSize: {0}", info.CommodityTickSize);
            Console.WriteLine("CommodityDenominator: {0}", info.CommodityDenominator);
            Console.WriteLine("CmbDirect: {0}", info.CmbDirect);
            Console.WriteLine("CommodityContractLen: {0}", info.CommodityContractLen);
            Console.WriteLine("IsDST: {0}", info.IsDST);
        }
        private static void commodityConvert(TapAPICommodity com1, out APICommodity com2)
        {
            com2.CommodityNo = Encoding.Default.GetString(com1.CommodityNo);
            com2.CommodityType = Convert.ToChar(com1.CommodityType);
            com2.ExchangeNo = Encoding.Default.GetString(com1.ExchangeNo);
        }

        private static void rspQryCommodityCB(UInt32 sessionID, int errcode, byte isLast, ref TapAPIQuoteCommodityInfo info)
        {
            APIQuoteCommodityInfo data;
            Console.Write("CallBack rspQryCommodityCB\n");
            commodityConvert(info.Commodity, out data.Commodity);
            commodityConvert(info.RelateCommodity1, out data.RelateCommodity1);
            commodityConvert(info.RelateCommodity2, out data.RelateCommodity2);
            data.CommodityName = Encoding.Default.GetString(info.CommodityName);
            data.CommodityEngName = Encoding.Default.GetString(info.CommodityEngName);
            data.ContractSize = info.ContractSize;
            data.CommodityTickSize = info.CommodityTickSize;
            data.CommodityDenominator = info.CommodityDenominator;
            data.CmbDirect = Convert.ToChar(info.CmbDirect);
            data.CommodityContractLen = info.CommodityContractLen;
            data.IsDST = Convert.ToChar(info.IsDST);
            dumpComInfo(data);
         }

        private static void rspQryContractCB(UInt32 sessionID, int errcode, byte isLast, ref TapAPIQuoteContractInfo info)
        {
            Console.Write("CallBack rspQryContractCB\n");
        }

        private static void rspSubscribeQuoteCB(UInt32 sessionID, int errcode, byte isLast, ref TapAPIQuoteWhole info)
        {
            Console.Write("CallBack rspSubscribeQuoteCB\n");
        }

        private static void rspUnSubscribeQuoteCB(UInt32 sessionID, int errcode, byte isLast, ref TapAPIContract info)
        {
            Console.Write("CallBack rspUnSubscribeQuoteCB\n");
        }

        private static void rtnQuoteCB(ref TapAPIQuoteWhole info)
        {
            Console.Write("CallBack rtnQuoteCB\n");
            Console.WriteLine("TimeStamp： {0}", Encoding.Default.GetString(info.DateTimeStamp));
        }

        public QuoteAPI(string authCode, string keyOperation)
        {
            TapAPIApplicationInfo applicationInfo;
            Int32 iResult = 0;
            byte[] temp;
            int len;
            apiVersion = Marshal.PtrToStringAnsi(GetQuoteAPIVersion());

            Console.Write("QuoteAPI Version: {0}\n", apiVersion);

            applicationInfo.AuthCode = new byte[513];
            temp = Encoding.Default.GetBytes(authCode);
            len = Encoding.Default.GetByteCount(authCode);
            ByteToByte(applicationInfo.AuthCode, 513, temp, len);


            applicationInfo.KeyOperationLogPath = new byte[301];
            temp = Encoding.Default.GetBytes(keyOperation);
            len = Encoding.Default.GetByteCount(keyOperation);
            ByteToByte(applicationInfo.KeyOperationLogPath, 301, temp, len);


            IntPtr applicationInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(applicationInfo));
            Marshal.StructureToPtr(applicationInfo, applicationInfoPtr, false);

            quotePtr = CreateQuoteAPI(applicationInfoPtr, ref iResult);

            if (quotePtr == IntPtr.Zero)
            {
                Console.Write("Create Quote failure. ErrCode: {0}\n", iResult);
                return;
            }

            quoteNotifyPtr = CreateNotifyAPI();
            SetAPINotify(quotePtr, quoteNotifyPtr);
            SetAPIReadyCB(quoteNotifyPtr, apiReadyCB);
            SetDisconnectCB(quoteNotifyPtr, disconnectCB);
            SetRspLoginCB(quoteNotifyPtr, rspLoginCB);
            SetRspQryCommodityCB(quoteNotifyPtr, rspQryCommodityCB);
            SetRspQryContractCB(quoteNotifyPtr, rspQryContractCB);
            SetRspSubscribeQuoteCB(quoteNotifyPtr, rspSubscribeQuoteCB);
            SetRspUnSubscribeQuoteCB(quoteNotifyPtr, rspUnSubscribeQuoteCB);
            SetRtnQuoteCB(quoteNotifyPtr, rtnQuoteCB);
        }

        ~QuoteAPI()
        {
            FreeQuoteAPI(quotePtr);
        }

        public int SetAPIDataPath(string path)
        {
            return SetQuoteAPIDataPath(path);
        }

        public int SetAPILogLevel(char logLevel)
        {
            return SetQuoteAPILogLevel(Convert.ToByte(logLevel));
        }

        public int ConnectQuoteServer(string ip, ushort port)
        {
            return SetQuoteHostAddress(quotePtr, ip, port);
        }

        public int Login(APIQuoteLoginAuth apiLoginAuth)
        {
            TapAPIQuoteLoginAuth loginAuth;
            byte[] temp;
            int len;

            loginAuth.UserNo = new byte[21];
            temp = Encoding.Default.GetBytes(apiLoginAuth.UserNo);
            len = Encoding.Default.GetByteCount(apiLoginAuth.UserNo);
            ByteToByte(loginAuth.UserNo, 21, temp, len);

            loginAuth.Password = new byte[21];
            temp = Encoding.Default.GetBytes(apiLoginAuth.Password);
            len = Encoding.Default.GetByteCount(apiLoginAuth.Password);
            ByteToByte(loginAuth.Password, 21, temp, len);

            loginAuth.ISModifyPassword = Convert.ToByte(apiLoginAuth.ISModifyPassword);

            loginAuth.NewPassword = new byte[21];
            temp = Encoding.Default.GetBytes(apiLoginAuth.NewPassword);
            len = Encoding.Default.GetByteCount(apiLoginAuth.NewPassword);
            ByteToByte(loginAuth.NewPassword, 21, temp, len);

            loginAuth.QuoteTempPassword = new byte[21];
            temp = Encoding.Default.GetBytes(apiLoginAuth.QuoteTempPassword);
            len = Encoding.Default.GetByteCount(apiLoginAuth.QuoteTempPassword);
            ByteToByte(loginAuth.QuoteTempPassword, 21, temp, len);

            loginAuth.ISDDA = Convert.ToByte(apiLoginAuth.ISDDA);

            loginAuth.DDASerialNo = new byte[31];
            temp = Encoding.Default.GetBytes(apiLoginAuth.DDASerialNo);
            len = Encoding.Default.GetByteCount(apiLoginAuth.DDASerialNo);
            ByteToByte(loginAuth.DDASerialNo, 31, temp, len);

            IntPtr loginAuthPtr = Marshal.AllocHGlobal(Marshal.SizeOf(loginAuth));
            Marshal.StructureToPtr(loginAuth, loginAuthPtr, false);

            return LoginQuote(quotePtr, loginAuthPtr);
        }

        public int Disconnect()
        {
            return DisconnectQuote(quotePtr);
        }

        public int QryCommodity(out UInt32 sessionID)
        {
            return QryCommodityQuote(quotePtr, out sessionID);
        }

        public int QryContract(UInt32 sessionID, APICommodity apiQryReq)
        {
            TapAPICommodity qryReq;
            byte[] temp;
            int len;

            qryReq.ExchangeNo = new byte[11];
            temp = Encoding.Default.GetBytes(apiQryReq.ExchangeNo);
            len = Encoding.Default.GetByteCount(apiQryReq.ExchangeNo);
            ByteToByte(qryReq.ExchangeNo, 11, temp, len);

            qryReq.CommodityNo = new byte[11];
            temp = Encoding.Default.GetBytes(apiQryReq.CommodityNo);
            len = Encoding.Default.GetByteCount(apiQryReq.CommodityNo);
            ByteToByte(qryReq.CommodityNo, 11, temp, len);

            qryReq.CommodityType = Convert.ToByte(apiQryReq.CommodityType);


            IntPtr qryReqPtr = Marshal.AllocHGlobal(Marshal.SizeOf(qryReq)); 
            Marshal.StructureToPtr(qryReq, qryReqPtr, false);

            return QryContractQuote(quotePtr, ref sessionID,  qryReqPtr);
        }

        public int SubscribeQuote(UInt32 sessionID, APIContract apiContract)
        {
            TapAPIContract contract;
            byte[] temp;
            int len;

            contract.Commodity.ExchangeNo = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.Commodity.ExchangeNo);
            len = Encoding.Default.GetByteCount(apiContract.Commodity.ExchangeNo);
            ByteToByte(contract.Commodity.ExchangeNo, 11, temp, len);

            contract.Commodity.CommodityNo = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.Commodity.CommodityNo);
            len = Encoding.Default.GetByteCount(apiContract.Commodity.CommodityNo);
            ByteToByte(contract.Commodity.CommodityNo, 11, temp, len);

            contract.Commodity.CommodityType = Convert.ToByte(apiContract.Commodity.CommodityType);
            contract.CallOrPutFlag1 = Convert.ToByte(apiContract.CallOrPutFlag1);
            contract.CallOrPutFlag2 = Convert.ToByte(apiContract.CallOrPutFlag2);

            contract.ContractNo1 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.ContractNo1);
            len = Encoding.Default.GetByteCount(apiContract.ContractNo1);
            ByteToByte(contract.ContractNo1, 11, temp, len);

            contract.ContractNo2 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.ContractNo2);
            len = Encoding.Default.GetByteCount(apiContract.ContractNo2);
            ByteToByte(contract.ContractNo2, 11, temp, len);

            contract.StrikePrice1 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.StrikePrice1);
            len = Encoding.Default.GetByteCount(apiContract.StrikePrice1);
            ByteToByte(contract.StrikePrice1, 11, temp, len);

            contract.StrikePrice2 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.StrikePrice2);
            len = Encoding.Default.GetByteCount(apiContract.StrikePrice2);
            ByteToByte(contract.StrikePrice2, 11, temp, len);

            IntPtr contractPtr = Marshal.AllocHGlobal(Marshal.SizeOf(contract));
            Marshal.StructureToPtr(contract, contractPtr, false);

            return Subscribe(quotePtr, ref sessionID, contractPtr);
        }

        public int UnSubscribeQuote(UInt32 sessionID, APIContract apiContract)
        {
            TapAPIContract contract;
            byte[] temp;
            int len;

            contract.Commodity.ExchangeNo = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.Commodity.ExchangeNo);
            len = Encoding.Default.GetByteCount(apiContract.Commodity.ExchangeNo);
            ByteToByte(contract.Commodity.ExchangeNo, 11, temp, len);

            contract.Commodity.CommodityNo = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.Commodity.CommodityNo);
            len = Encoding.Default.GetByteCount(apiContract.Commodity.CommodityNo);
            ByteToByte(contract.Commodity.CommodityNo, 11, temp, len);

            contract.Commodity.CommodityType = Convert.ToByte(apiContract.Commodity.CommodityType);
            contract.CallOrPutFlag1 = Convert.ToByte(apiContract.CallOrPutFlag1);
            contract.CallOrPutFlag2 = Convert.ToByte(apiContract.CallOrPutFlag2);

            contract.ContractNo1 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.ContractNo1);
            len = Encoding.Default.GetByteCount(apiContract.ContractNo1);
            ByteToByte(contract.ContractNo1, 11, temp, len);

            contract.ContractNo2 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.ContractNo2);
            len = Encoding.Default.GetByteCount(apiContract.ContractNo2);
            ByteToByte(contract.ContractNo2, 11, temp, len);

            contract.StrikePrice1 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.StrikePrice1);
            len = Encoding.Default.GetByteCount(apiContract.StrikePrice1);
            ByteToByte(contract.StrikePrice1, 11, temp, len);

            contract.StrikePrice2 = new byte[11];
            temp = Encoding.Default.GetBytes(apiContract.StrikePrice2);
            len = Encoding.Default.GetByteCount(apiContract.StrikePrice2);
            ByteToByte(contract.StrikePrice2, 11, temp, len);

            IntPtr contractPtr = Marshal.AllocHGlobal(Marshal.SizeOf(contract));
            Marshal.StructureToPtr(contract, contractPtr, false);

            return UnSubscribe(quotePtr, ref sessionID, contractPtr);
        }


        static void ByteToByte(byte[] s1, int len1, byte[] s2, int len2)
        {
            int i;
            for (i = 0; i < len2; i++)
            {
                if (i >= len1)
                {
                    break;
                }
                s1[i] = s2[i];
            }
        }

    }
    
    class Program
    {
        static string DEFAULT_AUTHCODE = "67EA896065459BECDFDB924B29CB7DF1946CED32E26C1EAC946CED32E26C1EAC946CED32E26C1EAC946CED32E26C1EAC5211AF9FEE541DDE41BCBAB68D525B0D111A0884D847D57163FF7F329FA574E7946CED32E26C1EAC946CED32E26C1EAC733827B0CE853869ABD9B8F170E14F8847D3EA0BF4E191F5D97B3DFE4CCB1F01842DD2B3EA2F4B20CAD19B8347719B7E20EA1FA7A3D1BFEFF22290F4B5C43E6C520ED5A40EC1D50ACDF342F46A92CCF87AEE6D73542C42EC17818349C7DEDAB0E4DB16977714F873D505029E27B3D57EB92D5BEDA0A710197EB67F94BB1892B30F58A3F211D9C3B3839BE2D73FD08DD776B9188654853DDA57675EBB7D6FBBFC";
        static string DEFAULT_IP = "123.15.58.21";
        static ushort DEFAULT_PORT = 7171;
        static string DEFAULT_USERNAME = "ESUNNY";
        static string DEFAULT_PASSWORD = "Es123456";
        static string DEFAULT_EXCHANGE_NO = ("HKEX");
        static string DEFAULT_COMMODITY_NO = "HSI";
        static string DEFAULT_CONTRACT_NO = "1604";


        static void Main(string[] args)
        {
            int err = 0;
            Log myLog = new Log(@"D:\GitHub\myCSDemop\debugLog", "debug.log");

            myLog.WriteLog("Create QuoteAPI Object");
 
            QuoteAPI quoteObj = new QuoteAPI(DEFAULT_AUTHCODE, @"D:\GitHub\myCSDemop\log");

            quoteObj.SetAPIDataPath(@"D:\GitHub\myCSDemop\log");
            quoteObj.SetAPILogLevel('D');
           
            /*connect to server*/
            Console.Write("Connect to Server\n");
            err = quoteObj.ConnectQuoteServer(DEFAULT_IP, DEFAULT_PORT);
            if (err != 0)
            {
                myLog.WriteLog("Connect to Server Error: %d\n", err);
                return;
            }

            /*login server*/
            Console.Write("Login to Server\n");

            APIQuoteLoginAuth apiLoginAuth;
            apiLoginAuth.UserNo = DEFAULT_USERNAME;
            apiLoginAuth.Password = DEFAULT_PASSWORD;
            apiLoginAuth.ISModifyPassword = 'N';
            apiLoginAuth.NewPassword = String.Empty;
            apiLoginAuth.QuoteTempPassword = String.Empty;
            apiLoginAuth.ISDDA = 'N';
            apiLoginAuth.DDASerialNo = String.Empty;

            err = quoteObj.Login(apiLoginAuth);
            if (err != 0)
            {
                Console.Write("Login Error: %d\n", err);
                return;
            }
            Thread.Sleep(2000);
            /*获取所有品种*/
            UInt32 sessionID;
            err = quoteObj.QryCommodity(out sessionID);
            if (err != 0)
            {
                Console.Write("Get all commodity fail， errCode: {0}", err);
                Console.ReadKey();
                return;
            }
            else
            {
                Console.Write("SessionID: {0}\n", sessionID);
            }

            /*获取合约*/
          
            APICommodity com;
            com.CommodityNo = DEFAULT_COMMODITY_NO;
            com.ExchangeNo = DEFAULT_EXCHANGE_NO;
            com.CommodityType = 'F';
            quoteObj.QryContract(0, com);


            /*订阅合约*/
            APIContract stContract;
            stContract.Commodity.ExchangeNo = DEFAULT_EXCHANGE_NO;
            stContract.Commodity.CommodityType = 'F';
            stContract.Commodity.CommodityNo = DEFAULT_COMMODITY_NO;
            stContract.ContractNo1 = "1612";
            stContract.ContractNo2 = "1619";
            stContract.StrikePrice1 = String.Empty;
            stContract.StrikePrice2 = String.Empty;
            stContract.CallOrPutFlag1 = 'N';
            stContract.CallOrPutFlag2 = 'N';
            quoteObj.SubscribeQuote(sessionID, stContract);

            /*退订合约*/
            Console.ReadKey();
            quoteObj.UnSubscribeQuote(sessionID, stContract);

            /*断开连接*/
            Console.ReadKey();
            quoteObj.Disconnect();

            Console.ReadKey();
        }
    }
}
