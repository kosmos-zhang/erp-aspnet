function httpRequst(url){
     var oBao = new ActiveXObject("Microsoft.XMLHTTP"); 
     oBao.open("POST",url,false); 
     oBao.send(); 
     var strResult = oBao.responseText;
     return strResult;
}

var XMLHttp =
{
    /*
    * 定义第一个属性，该属性用户缓存XMLHttpRequest对象的数组
    */
	XMLHttpRequestPool: [],
	getInstance:function() {
		//从XMLHttpRequest对象池中取出一个空闲的XMLHttpRequest
		for (var i = 0; i < this.XMLHttpRequestPool.length; i++)
		{
			//如果XMLHttpRequest的readyStatus为0或者4，表示当前的XMLHttpRequest为空闲的对象
			if (this.XMLHttpRequestPool[i].readyState == 0 || this.XMLHttpRequestPool[i].readyState == 4)
			{
				return this.XMLHttpRequestPool[i];
			}
		}
		//如果没有空闲的，则再次创建一个新的XMLHttpRequest对象
		this.XMLHttpRequestPool[this.XMLHttpRequestPool.length] = this.createXMLHttpRequest();
		//返回刚刚创建的XMLHttpRequest对象
		return this.XMLHttpRequestPool[this.XMLHttpRequestPool.length - 1];
	},
	/*
    * 创建XMLHttpRequst对象
    */
	createXMLHttpRequest:function()
	{
		//对于Mozilla，FireFox和Opera等浏览器
		if (window.XMLHttpRequest)
		{
			var objXMLHttp = new XMLHttpRequest();
		}
		//对于InternetExplorer浏览器
		else
		{
			//将Internet Explorer内置的所有XMLHTTP ActiveX控件设置成数组
			var MSXML = ['MSXML2.XMLHTTP.5.0', 'MSXML2.XMLHTTP.4.0', 'MSXML2.XMLHTTP.3.0'
			, 'MSXML2.XMLHTTP', 'Micorosoft.XMLHTTP'];
			//依次对Internet Explorer内置的XMLHTTP 控件初始化，从而保证获得XMLHttpRequest对象
			for (var n = 0; n < MSXML.length; n++)
			{
				try
				{
					//如果可以正常创建XMLHttpRequest对象，则使用break跳出循环
					var objXMLHttp = new ActiveXObject(MSXML[n]);
					break;
				}
				catch (e)
				{
					alert(e);
				}
			}
			//Mozilla某些版本没有readyState属性
			if (objXMLHttp.readyState)
			{
				//直接设置其readyState为0
				objXMLHttp.readyState = 0;
				//对于没有readyState属性的浏览器，将load动作与下面的函数关联起来
				objXMLHttp.addEventListener("load", function() {
					//当从服务器加载数据完成后，将readyState状态设为4
					objXMLHttp.readyState = 4;
					if (typeof objXMLHttp.onreadystatechange == "function")
					{
						objXMLHttp.onreadystatechange();
					}
				}, false);
			}
		}
		return objXMLHttp;
	},
    /*
    * 发送请求提交数据
    * method 传递方式(GET,POST)
    * url http字符串包括查询字符串
    * data 发送的数据 没有就传 null或''
    * callBack 回调函数
    */
	sendRequest:function(method, url, data, callback)
	{
		var objXMLHttp = this.getInstance();
		with (objXMLHttp)
		{
			try
			{
				//加随机数防止缓存，主要目的是防止直接从浏览器读取数据
				if (url.indexOf("?") > 0)
				{
					url += "&randnum=" + Math.random();
				}
				else
				{
					url += "?randnum=" + Math.random();
				}
				//打开与服务器的连接
				open(method, url, true);
				//如果使用post请求
				if (method == "POST")
				{
					//设定请求头
					setRequestHeader('Content-Type', 'application/x-www-from-urlencoded');
					send(data);
				}
				//如果使用get请求
				if (method == "GET")
				{
					send(null);
				}
				//设置回调函数
				onreadystatechange = function ()
				{
					//当服务器的相应完成并获得了正常的服务器响应时
					if (objXMLHttp.readyState == 4 && (objXMLHttp.status == 200 || objXMLHttp.status == 304))
					{
						//当响应时机成熟时，调用回调函数处理响应
						if (callback != null)
						{
							callback(objXMLHttp);
						} else
						{
						    ProcessResponse(objXMLHttp); 
						}
					}
				}
			}
			catch (e)
			{
				alert(e);
			}
		}
	}
}

/*
* 提交数据
* method 传递方式(GET,POST)
* url http字符串包括查询字符串
* data 发送的数据 没有就传 null或''
* sendType 请求方式(true 异步 false 同步)
* callBack 回调函数
* dataType 编码方式(text/xml
*                   ,application/xml
*                   "application/x-www-form-urlencoded; charset=GB2312")
*/
function SendRequest(method, url, data, sendType, callBack, dataType) {
    XMLHttp.sendRequest(method, url, data, callBack);
}

/*
* 处理服务器返回的响应
* xmlHttp XMLHttpRequst对象
*/
function ProcessResponse(xmlHttp) {
}
