using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace myCSDemop
{
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
        byte [] ExchangeNo;                             ///< 交易所编码
        byte CommodityType;                          ///< 品种类型
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] CommodityNo;                            ///< 品种编号
    };

    //! 合约编码结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct TapAPIContract
    {
        TapAPICommodity Commodity;                              ///< 品种
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] ContractNo1;                            ///< 合约代码1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] StrikePrice1;                           ///< 执行价1
        byte CallOrPutFlag1;                         ///< 看涨看跌标示1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] ContractNo2;                            ///< 合约代码2
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] StrikePrice2;                           ///< 执行价2
        char CallOrPutFlag2;                         ///< 看涨看跌标示2
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
        UInt32 LastLoginPort;                   ///< 上次登录使用的端口
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
        TapAPICommodity Commodity;                          ///< 品种
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        byte [] CommodityName;                       ///< 品种名称,GBK编码格式
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
        byte [] CommodityEngName;                    ///< 品种英文名称
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65)]
        byte [] ContractSize;                        ///< 每手乘数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65)]
        byte [] CommodityTickSize;                   ///< 最小变动价位
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
        byte [] CommodityDenominator;             ///< 报价分母
        byte CmbDirect;                         ///< 组合方向
        Int32 CommodityContractLen;             ///< 品种合约年限
        byte IsDST;                               ///< 是否夏令时,'Y'为是,'N'为否
        TapAPICommodity RelateCommodity1;                   ///< 关联品种1
        TapAPICommodity RelateCommodity2;                   ///< 关联品种2
    };


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
        TapAPIContract Contract;                        ///< 合约
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        byte [] CurrencyNo;                      ///< 币种编号
        byte TradingState;                  ///< 交易状态。1,集合竞价;2,集合竞价撮合;3,连续交易;4,交易暂停;5,闭市
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        byte [] DateTimeStamp;                  ///< 时间戳
        double QPreClosingPrice;                ///< 昨收盘价
        double QPreSettlePrice;             ///< 昨结算价
        UInt64 QPrePositionQty;                ///< 昨持仓量
        double QOpeningPrice;                   ///< 开盘价
        double QLastPrice;                      ///< 最新价
        double QHighPrice;                      ///< 最高价
        double QLowPrice;                       ///< 最低价
        double QHisHighPrice;                   ///< 历史最高价
        double QHisLowPrice;                    ///< 历史最低价
        double QLimitUpPrice;                   ///< 涨停价
        double QLimitDownPrice;             ///< 跌停价
        UInt64 QTotalQty;                      ///< 当日总成交量
        double QTotalTurnover;                  ///< 当日成交金额
        UInt64 QPositionQty;                   ///< 持仓量
        double QAveragePrice;                   ///< 均价
        double QClosingPrice;                   ///< 收盘价
        double QSettlePrice;                    ///< 结算价
        UInt64 QLastQty;                       ///< 最新成交量
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        double[] QBidPrice;                   ///< 买价1-20档
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        UInt64[] QBidQty;                    ///< 买量1-20档
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        double[] QAskPrice;                   ///< 卖价1-20档
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        UInt64[] QAskQty;                    ///< 卖量1-20档
        double QImpliedBidPrice;                ///< 隐含买价
        UInt64 QImpliedBidQty;                 ///< 隐含买量
        double QImpliedAskPrice;                ///< 隐含卖价
        UInt64 QImpliedAskQty;                 ///< 隐含卖量
        double QPreDelta;                       ///< 昨虚实度
        double QCurrDelta;                      ///< 今虚实度
        UInt64 QInsideQty;                     ///< 内盘量
        UInt64 QOutsideQty;                    ///< 外盘量
        double QTurnoverRate;                   ///< 换手率
        UInt64 Q5DAvgQty;                      ///< 五日均量
        double QPERatio;                        ///< 市盈率
        double QTotalValue;                 ///< 总市值
        double QNegotiableValue;                ///< 流通市值
        Int64 QPositionTrend;                   ///< 持仓走势
        double QChangeSpeed;                    ///< 涨速
        double QChangeRate;                 ///< 涨幅
        double QChangeValue;                    ///< 涨跌值
        double QSwing;                          ///< 振幅
        UInt64 QTotalBidQty;                   ///< 委买总量
        UInt64 QTotalAskQty;                   ///< 委卖总量
    };

    //! Test
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct Test
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public int[] array;         
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
        public static extern int SetQuoteHostAddress(IntPtr apiObj, string ip, ushort port);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoginQuote(IntPtr apiObj,  IntPtr  loginAuth);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DisconnectQuote(IntPtr apiObj);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int QryCommodityQuote(IntPtr apiObj, out UInt32 sessionID);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int QryContractQuote(IntPtr apiObj, IntPtr sessionID, string exchangeNo, char commodityType, string commodityNo);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Subscribe(IntPtr apiObj, IntPtr sessionID, IntPtr commodity, string contractNo1, string strikePrice1, char flag1, string contractNo2, string strikePrice2, char flag2);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int UnSubscribe(IntPtr apiObj, IntPtr sessionID, IntPtr commodity, string contractNo1, string strikePrice1, char flag1, string contractNo2, string strikePrice2, char flag2);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAPIReadyCB(IntPtr notifyObj, CallbackDelegateApiReady callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDisconnectCB(IntPtr notifyObj, CallbackDelegateDisconnect callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRspLoginCB(IntPtr notifyObj, CallbackDelegateRspLogin callback);

        [DllImport("Test.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRspQryCommodityCB(IntPtr notifyObj, CallbackDelegateRspQryCommodity callback);

        public delegate void CallbackDelegateApiReady();

        public delegate void CallbackDelegateDisconnect(int errCode);

        public delegate void CallbackDelegateRspLogin(int errCode, ref TapAPIQuotLoginRspInfo info);

        public delegate void CallbackDelegateRspQryCommodity(UInt32 sessionID, int errCode, byte isLast, ref TapAPIQuoteCommodityInfo info);
        /*The following functions are callback implementated by ourselves*/

        private static void apiReadyCB()
        {
            Console.Write("Call Back apiReadyCB\n");
        }

        private static void disconnectCB(int errCode)
        {
            Console.Write("Call Back disconnectCB: {0}\n", errCode);
        }

        private static void rspLoginCB(int errCode,  ref TapAPIQuotLoginRspInfo info)
        {
            Console.Write("Call Back rspLoginCB\n");
            Console.Write("ErrCode: {0}\n", errCode);
            Console.Write("UserName {0}\n", Encoding.ASCII.GetString(info.UserNo));
        }

        private static void rspQryCommodityCB(UInt32 sessionID, int errcode, byte isLast, ref TapAPIQuoteCommodityInfo info)
        {
            Console.Write("Call Back rspQryCommodityCB\n");
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

            quoteNotifyPtr = CreateNotifyAPI();
            SetAPINotify(quotePtr, quoteNotifyPtr);
            SetAPIReadyCB(quoteNotifyPtr, apiReadyCB);
            SetDisconnectCB(quoteNotifyPtr, disconnectCB);
            SetRspLoginCB(quoteNotifyPtr, rspLoginCB);
            SetRspQryCommodityCB(quoteNotifyPtr, rspQryCommodityCB);

        }

        ~QuoteAPI()
        {
            FreeQuoteAPI(quotePtr);
        }

        public int ConnectQuoteServer(string ip, ushort port)
        {
            return SetQuoteHostAddress(quotePtr, ip, port);
        }

        // public int Login(string userNo, char isModifyPassword, string password, string newPassword, string tempPassword, char isDDA, string DDASerialNo)
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

            loginAuth.ISDDA = Convert.ToByte('N');

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

        public int QryContract(IntPtr sessionID, string exchangeNo, char commodityType, string commodityNo)
        {
            return QryContractQuote(quotePtr, sessionID, exchangeNo, commodityType, commodityNo);
        }

        public int SubscribeQuote(IntPtr sessionID, IntPtr commodity, string contractNo1, string strikePrice1, char flag1, string contractNo2, string strikePrice2, char flag2)
        {
            return Subscribe(quotePtr, sessionID, commodity, contractNo1, strikePrice1, flag1, contractNo2, strikePrice2, flag2);
        }

        public int UnSubscribeQuote(IntPtr sessionID, IntPtr commodity, string contractNo1, string strikePrice1, char flag1, string contractNo2, string strikePrice2, char flag2)
        {
            return UnSubscribe(quotePtr, sessionID, commodity, contractNo1, strikePrice1, flag1, contractNo2, strikePrice2, flag2);
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

        static void Main(string[] args)
        {
            int err;
            Test test;
            test.array = new int[10];

            Console.Write("size: {0}", Marshal.SizeOf(test));

            Console.Write("Create QuoteAPI Object\n");
            QuoteAPI quoteObj = new QuoteAPI(DEFAULT_AUTHCODE, "");
       
            /*connect to server*/
            Console.Write("Connect to Server\n");
            err = quoteObj.ConnectQuoteServer(DEFAULT_IP, DEFAULT_PORT);
            if (err != 0)
            {
                Console.Write("Connect to Server Error: %d\n", err);
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

            Console.ReadKey();
            quoteObj.Disconnect();

            Console.ReadKey();
        }
    }
}
