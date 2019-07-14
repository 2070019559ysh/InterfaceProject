using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceProject.WxSDK.HelpModel
{
    /// <summary>
    /// 封装要发送的模板消息参数
    /// <para>注：url和miniprogram都是非必填字段，若都不传则模板无跳转；若都传，会优先跳转至小程序。开发者可根据实际需要选择其中一种跳转方式即可。当用户的微信客户端版本不支持跳小程序时，将会跳转至url。</para>
    /// </summary>
    public class TemplateMessageParam
    {
        /// <summary>
        /// 接收者openid
        /// </summary>
        public string touser;
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id;
        /// <summary>
        /// 模板跳转链接
        /// </summary>
        public string url;
        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        public MiniProgramParam miniprogram;
        /// <summary>
        /// 用于替换模板参数的关键字数据
        /// </summary>
        public TemplateKeyWord data;
    }

    public class MiniProgramParam
    {
        /// <summary>
        /// 所需跳转到的小程序appid（该小程序appid必须与发模板消息的公众号是绑定关联关系，暂不支持小游戏）
        /// </summary>
        public string appid;
        /// <summary>
        /// 所需跳转到小程序的具体页面路径，支持带参数,（示例index?foo=bar），暂不支持小游戏
        /// </summary>
        public string pagepath;
    }

    /// <summary>
    /// 微信模板消息用于赋值的关键字封装对象
    /// </summary>
    public class TemplateKeyWord
    {
        /// <summary>
        /// 第一句话
        /// </summary>
        public TKeyWord first;
        /// <summary>
        /// 中间的多个关键字
        /// </summary>
        public List<TKeyWord> keyWords = new List<TKeyWord>();
        /// <summary>
        /// 最后的备注
        /// </summary>
        public TKeyWord remark;
    }

    /// <summary>
    /// 目标消息关键字信息
    /// </summary>
    public class TKeyWord
    {
        /// <summary>
        /// 模板关键子内容
        /// </summary>
        public string value;
        /// <summary>
        /// 模板内容字体颜色，不填默认为黑色
        /// </summary>
        public string color = "#173177";
    }

    /// <summary>
    /// 模板列表信息封装
    /// </summary>
    public class TemplateListInfo
    {
        public List<TemplateInfo> template_list;
    }

    /// <summary>
    /// 微信消息模板
    /// </summary>
    public class TemplateInfo
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id;
        /// <summary>
        /// 模板标题
        /// </summary>
        public string title;
        /// <summary>
        /// 模板所属行业的一级行业
        /// </summary>
        public string primary_industry;
        /// <summary>
        /// 模板所属行业的二级行业
        /// </summary>
        public string deputy_industry;
        /// <summary>
        /// 模板内容
        /// </summary>
        public string content;
        /// <summary>
        /// 模板示例
        /// </summary>
        public string example;
    }

    /// <summary>
    /// 封装个单独消息模板Id
    /// </summary>
    public class TemplateID
    {
        /// <summary>
        /// 消息模板Id
        /// </summary>
        public string template_id;
    }

    /// <summary>
    /// 微信公众号行业查询信息
    /// </summary>
    public class IndustryQuery
    {
        /// <summary>
        /// 帐号设置的主营行业
        /// </summary>
        public SimpleInfo primary_industry;
        /// <summary>
        /// 帐号设置的副营行业
        /// </summary>
        public SimpleInfo secondary_industry;
    }

    /// <summary>
    /// 简单信息封装
    /// </summary>
    public class SimpleInfo
    {
        /// <summary>
        /// 第一条信息
        /// </summary>
        public string first_class;
        /// <summary>
        /// 第二条信息
        /// </summary>
        public string second_class;
    }

    /// <summary>
    /// 微信公众号所属行业
    /// </summary>
    public enum WeChatIndustry
    {
        /// <summary>
        /// IT科技--互联网/电子商务
        /// </summary>
        InternetAndE_Commerce=1,
        /// <summary>
        /// IT科技--IT软件与服务
        /// </summary>
        ITSoftwareAndServices,
        /// <summary>
        /// IT科技--IT硬件与设备
        /// </summary>
        ITHardwareAndEquipment,
        /// <summary>
        /// IT科技--电子技术
        /// </summary>
        ElectronicTechnique,
        /// <summary>
        /// IT科技--通信与运营商
        /// </summary>
        CommunicationsAndOperators,
        /// <summary>
        /// IT科技--网络游戏
        /// </summary>
        NetworkGame,
        /// <summary>
        /// 金融业--银行
        /// </summary>
        Bank,
        /// <summary>
        /// 金融业--基金理财信托
        /// </summary>
        FundManagementTrust,
        /// <summary>
        /// 金融业--保险
        /// </summary>
        Insurance,
        /// <summary>
        /// 餐饮--餐饮
        /// </summary>
        Restaurant,
        /// <summary>
        /// 酒店旅游--酒店
        /// </summary>
        Hotel,
        /// <summary>
        /// 酒店旅游--旅游
        /// </summary>
        Tourism,
        /// <summary>
        /// 运输与仓储--快递
        /// </summary>
        Express,
        /// <summary>
        /// 运输与仓储--物流
        /// </summary>
        GoodsFlow,
        /// <summary>
        /// 运输与仓储--仓储
        /// </summary>
        Storage,
        /// <summary>
        /// 教育--培训
        /// </summary>
        Train,
        /// <summary>
        /// 教育--院校
        /// </summary>
        College,
        /// <summary>
        /// 政府与公共事业--学术科研
        /// </summary>
        AcademicResearch,
        /// <summary>
        /// 政府与公共事业--交警
        /// </summary>
        TrafficPolice,
        /// <summary>
        /// 政府与公共事业--博物馆
        /// </summary>
        Museum,
        /// <summary>
        /// 政府与公共事业--公共事业非盈利机构
        /// </summary>
        PublicService,
        /// <summary>
        /// 医药护理--医药医疗
        /// </summary>
        MedicalAndMedicalTreatment,
        /// <summary>
        /// 医药护理--护理美容
        /// </summary>
        NursingBeauty,
        /// <summary>
        /// 医药护理--保健与卫生
        /// </summary>
        HealthAndSafe,
        /// <summary>
        /// 交通工具--汽车相关
        /// </summary>
        CarRelated,
        /// <summary>
        /// 交通工具--摩托车相关
        /// </summary>
        MotorcycleRelated,
        /// <summary>
        /// 交通工具--火车相关
        /// </summary>
        TrainRelated,
        /// <summary>
        /// 交通工具--飞机相关
        /// </summary>
        AircraftRelated,
        /// <summary>
        /// 房地产--建筑
        /// </summary>
        Architecture,
        /// <summary>
        /// 房地产--物业
        /// </summary>
        Property,
        /// <summary>
        /// 消费品--消费品
        /// </summary>
        ConsumerGoods,
        /// <summary>
        /// 商业服务--法律
        /// </summary>
        Law,
        /// <summary>
        /// 商业服务--会展
        /// </summary>
        Exhibition,
        /// <summary>
        /// 商业服务--中介服务
        /// </summary>
        IntermediaryServices,
        /// <summary>
        /// 商业服务--认证
        /// </summary>
        Authentication,
        /// <summary>
        /// 商业服务--审计
        /// </summary>
        Audit,
        /// <summary>
        /// 文体娱乐--传媒
        /// </summary>
        Media,
        /// <summary>
        /// 文体娱乐--体育
        /// </summary>
        Sports,
        /// <summary>
        /// 文体娱乐--娱乐休闲
        /// </summary>
        Entertainment,
        /// <summary>
        /// 印刷--印刷
        /// </summary>
        Printing,
        /// <summary>
        /// 其他--其他
        /// </summary>
        Other
    }
}
