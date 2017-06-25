﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample_1
{
    public partial class Index : System.Web.UI.Page
    {
        //与微信公众账号后台的Token设置保持一致，区分大小写。
        private readonly string Token = "weixin";

        protected void Page_Load(object sender, EventArgs e)
        {
            Auth();
        }

        /// <summary>
        /// 处理微信服务器验证消息
        /// </summary>
        private void Auth()
        {
            string signature = Request["signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];

            if (Request.HttpMethod == "GET")
            {
                //get method - 仅在微信后台填写URL验证时触发
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    WriteContent(echostr); //返回随机字符串则表示验证通过
                }
                else
                {
                    WriteContent("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                                "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                }
                Response.End();
            }
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }
    }
}