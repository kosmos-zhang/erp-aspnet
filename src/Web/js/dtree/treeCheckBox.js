/**
* @描述 无限级可选择的树
* @作者 吴成好
* @创建时间 2006-03-06
* @修改时间 2006-06-02
* @version: 0.1  实现了树形显示，点击展开收起。2006-03-20
* @version: 0.2  更换了数据结构，rows改用Object存放，这样可以直接用key存取数据。2006-03-21
* @versioan: 0.22 做了demo.测试速度。 2006-03-22
* 经过测试内存，超过1000节点如果全部展开，那么会非常耗时，约3秒以上。而且内存狂占，约多占18MB.
* 全部展开的方法如果另外写的话，那么，可能cpu以及内存的占用都会降低。
* 对每个节点都要来一个.innerHTML=str的操作，这消耗了时间以及内存。
* 节点的html字符串连接的过程不知道是否包含有内存泄漏的地方，没有及时释放掉内存的地方。
*
**/
function treeCheckBox( Tname  , rows , rowsPidIndex )
{
  if(typeof(Tname) != "string" || Tname == "")
    throw(new Error(-1, '创建类实例的时候请把类实例的引用变量名传递进来！'));
  
	  //【property】
	  this.version				= '0.22'; //IE，FireFox兼容。2006-03-22
	  this.name					= Tname ; 
	  this.topid						= 0 ; //根id
		this.AllExpanded		={}; 
		this.drawed				= 0 ; //测试用
		this.useCheckBox		=true;
	  this.checkBoxName  = "menu[]";
	  this.checkBoxProperty = '' ;
	  this.checkBoxChecked = '';
	  this.rootTitle			= "根节点";
	  this.totalNodes		= 0; 
	  this.rows					= rows ;
	  this.rowsPidIndex  = rowsPidIndex ;
	  this.deepLimit			= 0 ;
	  this.breaki				= 10 ;
	  this.i							= 0;
	  this.iconPath			= 'images/';
	  this.icons    = {
		 L0        : 'L0.gif',  //┏
		 L1        : 'L1.gif',  //┣
		 L2        : 'L2.gif',  //┗
		 L3        : 'L3.gif',  //━
		 L4        : 'L4.gif',  //┃
		 PM0       : 'P0.gif',  //＋┏
		 PM1       : 'P1.gif',  //＋┣
		 PM2       : 'P2.gif',  //＋┗
		 PM3       : 'P3.gif',  //＋━
		 empty     : 'L5.gif',     //空白图
		 root      : 'root.gif',   //缺省的根节点图标
		 folder    : 'book.gif', //缺省的文件夹图标
		 file      : 'file.gif',   //缺省的文件图标
		 exit      : 'exit.gif'
	  };
	  this.iconsExpand = {  //存放节点图片在展开时的对应图片
		 PM0       : 'M0.gif',     //－┏
		 PM1       : 'M1.gif',     //－┣
		 PM2       : 'M2.gif',     //－┗
		 PM3       : 'M3.gif',     //－━
		 folder    : 'bookopen.gif',
		 exit      : 'exit.gif'
	  };
}

//树的单击事件处理函数
treeCheckBox.prototype.clickHandle = function(e)
{
  e = window.event || e; e = e.srcElement || e.target;
  //alert(e.tagName)
  switch(e.tagName)
  {
    case "IMG" :
		  pid=e.parentNode.parentNode.id;
		  pid=pid.substr(pid.lastIndexOf("_") + 1);
		  this.expand(pid);
      break;
    case "A" :      
      break;
    case "SPAN" :
      break;
	 case "INPUT" :
		 //return;
		 if(e.type=='checkbox'){
			this.clickCheckBox(e);
		}
      break;
    default :
      break;
  }	
};

//checkBox 单击事件
treeCheckBox.prototype.clickCheckBox = function( e )
{ 
	id=e.value;
	var checkd= e.checked;

	var pIndex = this.rowsPidIndex[id];
	var hasChild = (typeof(pIndex)!='undefined' );	
	var hasParent = false;
	if( id!=this.topid )
	{
		var hasParent = (this.rows[id]['pid'] !='undefined');
	}
	var area  = document.getElementById(this.name +"_node_son_"+ id);
   var drawed = (area.childNodes.length>0);
	
	//展开此id下所有子节点
	if(this.AllExpanded[id]!=1 && checkd){
		this.expandAll(id); 
		this.AllExpanded[id]=1;
		if(area.style.display=="none")
			area.style.display = "block";
	}
	//如果有子节点
	if(hasChild){
		var nodeId=this.name+"_node_"+id;
		var node = document.getElementById(nodeId);
		var checkBoxes = node.getElementsByTagName('input'); 
		var n=checkBoxes.length;
		for (var i=0; i<n; i++)
		{	chk = checkBoxes[i];
			if(chk.type="checkbox"){
				chk.checked= checkd;
			}
		}
	}//hasChild

	//如果有父节点

	if( hasParent && checkd ){
		this.selectParent(id);
		//alert(this.rows[id]['pid']);
	}
};

//选择父节点
treeCheckBox.prototype.selectParent = function( cid )
{
	var i=0;
	while(cid!=this.topid)
	{
		cid = this.rows[cid]['pid'];
		var o = document.getElementById(this.name+"_node_checkbox_"+cid);
		o.checked = true;
		//o.style.backgroundColor="#EFF";
		i++;if(i>100)break;
	}
}

//展开指定ID下的所有节点
treeCheckBox.prototype.expandAll = function( id )
{ 
var d = new Date().getTime();
var hasChild = this.testChild(id);
if(hasChild){
	this.expand(id);
	this.expandAllChildren(id);
}
//tO=document.getElementById('timeCosts');
//tO.innerHTML= ("展开耗时 = "+ (new Date().getTime()-d) + " 毫秒！\r\n有效节点总数 = "+this.rowsPidIndex[id].length);
};

//迭代展开所有子节点
treeCheckBox.prototype.expandAllChildren = function( id )
{
	pIndex = this.rowsPidIndex[id];
	for (var cidKey in pIndex){
		var cid = this.rowsPidIndex[id][cidKey];
		var hasChild = this.testChild(cid);
		if(hasChild){			
			this.expand(cid);
			this.AllExpanded[cid]=1;
			this.expandAllChildren(cid);
		}
	}	
}

/**
* 初始化树
* 画第一层节点
*/
treeCheckBox.prototype.toString = function( containObj )
{
	if( !this.drawed ){
	 this.setIconPath( this.iconPath );
	 this.setStyle();
	}else{
		this.drawed=1;
	}

	 var cid = this.topid;
	 if(this.useCheckBox){
			var checkBox = "<input type='checkbox'  id='"+this.name+"_node_checkbox_"+cid+"' ";
			
			if(this.checkBoxChecked !='' ){
				if(this.checkBoxChecked.search(","+cid+",") !=-1){
					checkBox +=" checked ";
				}
			}

			checkBox +=" name='"+this.checkBoxName+"' value='"+cid+"' />";
	 }else{
		 var checkBox = "";
	 }
	 var nodeImg = "<img id='"+this.name+"_node_pre_img_"+cid+"' src='"+this.icons.PM0.src+"'  />";
 //alert(nodeImg);
	var	str ="<div class='boxTree' id='"+this.name+"_node_"+cid+"' class='TreeNode'  onclick="+this.name+".clickHandle(event)  noWrap>\n";
	str +="<NOBR>";	
	str +="<span id='"+this.name+"_node_cnt_"+cid+"' class='TreePreImg'>\n";
	str +="<span id='"+this.name+"_node_pre_"+cid+"'><span";
	str +="></span>";
	str +="<span>"+nodeImg+"</span>";
	str +="</span>";
	str +="</span>";
	str +="<span id='"+this.name+"_node_text_"+cid+"' class='TreeText'>"+checkBox+this.rootTitle+"</span>";
	str +="</NOBR>";
	str +="<span id='"+this.name+"_node_son_"+cid+"' style='display:none'></span>";
	str +="</div>\n";
   containObj.innerHTML = str;
	pIndex = this.rowsPidIndex[this.topid];
	var hasChild = typeof(pIndex)!='undefined';
	if (hasChild)
	{	
		this.expandAll( this.topid );
		this.AllExpanded[this.topid]=1;
	}
};

//测试是否含子节点
treeCheckBox.prototype.testChild   = function( id )
{
	var hasChild = ( typeof( this.rowsPidIndex[id] )!='undefined' ); 
	if(hasChild)
	{
		if( this.rowsPidIndex[id].length==0 )
		 hasChild=false;
	}
	return hasChild;
}

//点击展开树节点的对应方法
treeCheckBox.prototype.expand   = function( id )
{
var d = new Date().getTime();
var hasChild = this.testChild(id); 
 var area  = document.getElementById(this.name +"_node_son_"+ id);
  if (area && hasChild)
  {
	  //alert(id);
    var icon  = this.icons.PM1; //+ 
    var iconE = this.iconsExpand.PM1; //-
    var expanded  = area.style.display != "none";
    var img   = document.getElementById(this.name +"_node_pre_img_"+ id);
	 if( expanded ){
		 if( img.src==this.iconsExpand.PM1.src ){
			img.src=this.icons.PM1.src
		 }else if( img.src==this.iconsExpand.PM2.src ){
			img.src=this.icons.PM2.src
		 }else if( img.src==this.iconsExpand.PM0.src ){
			img.src=this.icons.PM0.src
		 }
	 }else {
		 if( img.src==this.icons.PM1.src ){
			img.src=this.iconsExpand.PM1.src
		 }else if( img.src==this.icons.PM2.src ){
			img.src=this.iconsExpand.PM2.src
		 }	 else if( img.src==this.icons.PM0.src ){
			img.src=this.iconsExpand.PM0.src
		 }	 
	 }
	  area.style.display = expanded ? "none" : "block";
	 //是否已经画过了
	 var drawed = (area.childNodes.length>0);
	 if( !expanded && !drawed){
		 str = this.buildSon( id );
		 area.innerHTML=str;
	 }
   }else{
		//如果没有子节点
		//alert('no child');
	}
if(hasChild){
//tO=document.getElementById('timeCosts');tO.innerHTML= ("展开耗时 = "+ (new Date().getTime()-d) + " 毫秒！\r\n有效节点总数 = "+this.rowsPidIndex[id].length);
}
};

//画子节点。
treeCheckBox.prototype.buildSon   = function( id )
{
	//var d=	new Date().getTime();
	var pre=document.getElementById(this.name +"_node_pre_"+ id);
	var imgpre   = pre.childNodes[0].innerHTML; //父id的前缀
	sonStr='';
	str='';
	var pIndex = this.rowsPidIndex[id];	
	//alert(this.rowsPidIndex[id]);
	var totmain = pIndex.length-1;
	var toti=0;
	var isLast=false;
	var preImg=imgpre;		
	if(id!=this.topid)
	{//如果非第一层节点，则均有其前缀。或为|或为空格
		var fatherId=this.rows[id]['pid'];
		var father = this.rowsPidIndex[fatherId];
		lastIdIndex = father.length-1;
		fatherLast = father[lastIdIndex];
		var isLast = (id==fatherLast);
		if (!isLast) {
			imgTmp = "<IMG src='"+this.icons.L4.src+"' align=absMiddle>"; 
			preImg  += imgTmp  ;  //下一个前缀 │
		}
		else {
			imgTmp = "<IMG src='"+this.icons.empty.src+"' align=absMiddle>"; 
			preImg  += imgTmp; //L5.gif.blank char
		}
	}

	for (var cidKey in pIndex)
	{
		var cid = pIndex[cidKey];
		//alert(cid);break;		
		var son = this.rowsPidIndex[cid];
		var hasChild = this.testChild(cid);

		//节点前图片
		if (hasChild){//有子节点显示
			if (toti<totmain) {
				var nodeImg = "<img id='"+this.name+"_node_pre_img_"+cid+"' src='"+this.icons.PM1.src+"'  />";
				//├  +
			}
			else {
				var nodeImg = "<img id='"+this.name+"_node_pre_img_"+cid+"' src='"+this.icons.PM2.src+"'  />";
				//└ +
			}
		}
		else
		{ //无子节点显示
			if (toti<totmain) {
				var nodeImg = "<img id='"+this.name+"_node_pre_img_"+cid+"' src='"+this.icons.L1.src+"'  />";
				 //├
			}
			else {
				var nodeImg = "<img id='"+this.name+"_node_pre_img_"+cid+"' src='"+this.icons.L2.src+"'  />";
				//└
			}		
		}

		var cText=this.rows[cid]['title'];
		 if(this.useCheckBox){
				var checkBox = "<input type='checkbox'  id='"+this.name+"_node_checkbox_"+cid+"' ";
				
				if(this.checkBoxChecked !='' ){
					var cidChecked = (this.checkBoxChecked.indexOf(","+cid+",") !=-1);
					if( cidChecked ){
						checkBox +=" checked ";
					}
				}

				checkBox +=" name='"+this.checkBoxName+"' value='"+cid+"' />";
		 }else{
			 var checkBox = "";
		 }
		//alert(cText);break;
		toti++;
		str ="<div id='"+this.name+"_node_"+cid+"' class='TreeNode'  noWrap>\n";
		str +="<div id='"+this.name+"_node_cnt_"+cid+"'>\n";
		str +="<NOBR>";
		str +="<span id='"+this.name+"_node_pre_"+cid+"'  class='TreePreImg'><span";
		str +=">"+preImg+"</span>";
		str +="<span>"+nodeImg+"</span>";
		str +="</span>";
		str +="<span id='"+this.name+"_node_text_"+cid+"' class='TreeText'>"+checkBox+cText+"</span>";
		str +="</NOBR>";
		str +="</div>";
		str +="<div id='"+this.name+"_node_son_"+cid+"' style='display:none'></div>";
		str +="</div>\n";
		sonStr +=str;
	}
	return sonStr;
};
 /*
str +="<div id='tree_node_1' class='TreeNode'  noWrap>";
str +="<div id='tree_node_cnt_1'>";
str +="<NOBR>";
str +="<span id='tree_node_pre_1' class='TreePreImg'><span";
str +="><IMG src='' align=absMiddle></span>";
str +="<span><IMG id='tree_node_pre_img_1' src='' align=absMiddle></span>";
str +="</span>";
str +="<span id='tree_node_text_1' class='TreeText'>checkbox. a href..</span>";
str +="</NOBR>";
str +="</div>";
str +="<div id='tree_node_son_1' style='display:none'>子节点</div>";
str +="</div>";
 */

//本树将要用动的图片的字义及预载函数
//path 图片存放的路径名
//来自meizz的树，去掉判断图片加载的代码
treeCheckBox.prototype.setIconPath  = function(path)
{
  for(var i in this.icons)
  {
    var tmp = this.icons[i];
    this.icons[i] = new Image();
    this.icons[i].src = path + tmp;
  }
  for(var i in this.iconsExpand)
  {
    var tmp = this.iconsExpand[i];
    this.iconsExpand[i]=new Image();
    this.iconsExpand[i].src = path + tmp;
  }
};

//给树加上样式设置
treeCheckBox.prototype.setStyle = function()
{
  var style = "<style>\n";
  style +=".TreeNode{clear:both;}\n";
  style +=".TreeNode *{margin:0px;padding:0px}\n";
  style +=".TreePreImg{float:left}\n";
  style +=".TreeText{float:left; height:20px; line-height: 20px; font-family:verdana; color: #009;}\n";
  style += "<\/style>";
  document.write(style);
};

