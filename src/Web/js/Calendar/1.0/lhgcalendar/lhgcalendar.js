/*
 *@Generator -> Calendar Dialog Plugins - Date : 2009-8-1
 *@Copyright lhgcore.js (c) 2009 By LiHuiGang Reserved
 */

var calendar = function(b)
{
    var p = this._p = new J.panel(b);
	p.applnk( J.path() + 'lhgcalendar/lhgcalendar.css' ); J(p._main).acls('cal_panel');
	
	if(J.clean) lhgcore.clean.items( this, this.c_clean );
	
	this.createbody( this._p._doc, this._p._main ); J.nosel(this._p._doc.body);
	if(J.ie){try{document.execCommand('BackgroundImageCache',false,true);}catch(e){}};
};


J.exend( calendar.prototype, {
    get : function(obj)
	{
	    var ev = J.gtev(), e = ev.srcElement || ev.target, id;
		if( obj && obj.id ) id = J(obj.id).val(); else id = e.value;
		
		if( obj && obj.to )
		    this.to( obj.to );
		else
		    this.than = this.ty = this.tm = this.td = null;
		
		this.draw(id); this.inu = ( obj && obj.id ) ? obj.id : e;
		this.type = ( obj && obj.type ) ? obj.type : '-';
		this.time = ( obj && obj.time ) ? obj.time : false;
		
		if( obj && obj.dir == 'right' )
		    this._p.show( e.offsetWidth, 0, e );
		else
		    this._p.show( 0, e.offsetHeight, e );
		
		ev.stopPropagation ? ev.stopPropagation() : (ev.cancelBubble = true);
	},
	
	createbody: function(d,m)
	{
	    var chead = J(d).crte('div'), txt;
		txt = '<div id="date"><span id="year"></span>&nbsp;年&nbsp;<span id="month"></span>&nbsp;月</div>';
		txt = '<div id="py">&nbsp;</div><div id="pm">&nbsp;</div>' + txt + '<div id="ny">&nbsp;</div><div id="nm">&nbsp;</div>';
		
		J(chead).stcs({ width: '170px', height: '30px' }).html(txt); J(m).apch(chead);
		
		txt = '<table width="100%" cellspacing="0" cellpadding="0" border="0">' +
			  '<thead id="tdy"><tr><td>日</td><td>一</td><td>二</td><td>三</td><td>四</td><td>五</td><td>六</td></tr></thead>' +
			  '<tbody id="idcal"></tbody></table>' +
			  '<div id="foot"><span id="t1">今天</span><span id="t3">清空</span><span id="t2">' + new Date().toLocaleDateString() + '</span></div>';
		
		var cbody = J(d).crte('div'); J(cbody).stcs('width','170px').html(txt); J(m).apch(cbody);
		
		J('py',d).click(this.py); J('pm',d).click(this.pm); J('ny',d).click(this.ny);
		J('nm',d).click(this.nm); J('year',d).click(this.year); J('month',d).click(this.month);
		J('t1',d).hover(function(){
		    J(this).stcs({ border: '1px solid #0a246a', backgroundColor: '#c2cbe0' });
		},function(){
			J(this).stcs({ border: '1px solid #e3e3e3', backgroundColor: '#f7f7f7' });
		}).click(function(){
		    var d = new Date(), y = d.getFullYear(), m = d.getMonth() + 1, t = d.getDate();
		    if(m<=9)m="0"+m;if(t<=9)t="0"+t;
			if( J.calendar.type == '-')
				J(J.calendar.inu).val( y + '-' + m + '-' + t );
			else
				J(J.calendar.inu).val( m + '/' + t + '/' + y );
				J.calendar._p.hide( this, J.calendar._p );				
		});
		J('t3',d).hover(function(){
		    J(this).stcs({ border: '1px solid #0a246a', backgroundColor: '#c2cbe0' });
		},function(){
			J(this).stcs({ border: '1px solid #e3e3e3', backgroundColor: '#f7f7f7' });
		}).click(function(){ var c = J.calendar;
		    if( typeof c.inu == 'object' ) c.inu.value = '';
			else J('#'+c.inu).value = ''; c._p.hide( this, c._p ); J(c.inu).focus();
		});
	},
	draw : function(s)
	{
		var p, d = new Date(), a = [], fd, md, frag, doc = this._p._doc, cal = J('#idcal',doc);
		
		if( s && J.inde(s,'-') >= 0 )
		{
			p = J.trim(s).split('-'); p[2] = J.trim( p[2].substr(0,2) );
			d = new Date( p[0], p[1]-1, p[2] );
		}
		else if( s && J.inde(s,'/') >= 0 )
		{
			p = J.trim(s).split('/'); p[2] = J.trim( p[2].substr(0,4) );
			d = new Date( p[2], p[0]-1, p[1] );
		}
		
		this.y = d.getFullYear(); this.m = d.getMonth() + 1; this.d = d.getDate();

		fd = new Date(this.y, this.m - 1, 1).getDay();
		md = new Date(this.y, this.m, 0).getDate();
		
		for( var i = 1; i <= fd; i++ ) a.push(0);
		for( var i = 1; i <= md; i++ ) a.push(i);
		
		frag = doc.createDocumentFragment();
		while( a.length )
		{
		    var row = J(doc).crte('tr');
			for( var i = 0; i < 7; i++ )
			{
			    var cell = J(doc).crte('td'); J(cell).acls('tdday').html('&nbsp;');
				if( a.length )
				{
				    var day = a.shift();
					if(day)
					{
						if( this.than && this.than == 'min' )
						{
						    if( this.y < this.ty || (this.y == this.ty && this.m < this.tm) || (this.y == this.ty && this.m == this.tm && day < this.td) )
							{
						        if( day == this.d ) J(cell).acls('tday');
						        J(cell).html(day).msover(this.over).msout(this.out).click(this.setdate);
							}
							else
							    J(cell).html(day).stcs('color','#999');
						}
						else if( this.than && this.than == 'max' )
						{
						    if( this.y > this.ty || (this.y == this.ty && this.m > this.tm) || (this.y == this.ty && this.m == this.tm && day > this.td) )
							{
						        if( day == this.d ) J(cell).acls('tday');
						        J(cell).html(day).msover(this.over).msout(this.out).click(this.setdate);
							}
							else
							    J(cell).html(day).stcs('color','#999');
						}
						else
						{
						    if( day == this.d ) J(cell).acls('tday');
						    J(cell).html(day).msover(this.over).msout(this.out).click(this.setdate);
						}
					}
				} J(row).apch(cell);
			} J(frag).apch(row);
		}
		while( cal.hasChildNodes() ) J.rech(cal.firstChild);
		J(cal).apch(frag); J('year',doc).html(this.y); J('month',doc).html(this.m);
		var iw = this._p._main.offsetHeight; J(this._p._ifrm).stcs({height: iw + 'px'});
	},
	
	to : function(l)
	{
	    var a = l.split(','), v = J('#'+a[0]) ? J.trim(J(a[0]).val()) : a[0];
		if( J('#'+a[0]) && J(a[0]).isnl() )
		{
		    this.than = this.ty = this.tm = this.td = null; return;
		}
		var p = v.split('-'), d = new Date( p[0], p[1]-1, p[2] ); this.than = a[1];
		this.ty = d.getFullYear(); this.tm = d.getMonth() + 1; this.td = d.getDate();
	},
	
	py : function()
	{
	    J.calendar.predraw( new Date(J.calendar.y - 1, J.calendar.m - 1, J.calendar.d) );
	},
	
	pm : function()
	{
	    J.calendar.predraw( new Date(J.calendar.y, J.calendar.m - 2, J.calendar.d) );
	},
	
	ny : function()
	{
	    J.calendar.predraw( new Date(J.calendar.y + 1, J.calendar.m - 1, J.calendar.d) );
	},
	
	nm : function()
	{
	    J.calendar.predraw( new Date(J.calendar.y, J.calendar.m, J.calendar.d) );
	},
	
	cy : function()
	{
	    if( this.value < 1900 ) this.value = 1900; if( this.value > 2099 ) this.value = 2099;
		J.calendar.predraw( new Date(this.value, J.calendar.m - 1, J.calendar.d) );
	},
	
	cm : function()
	{
	    if( this.value < 1 ) this.value = 1; if( this.value > 12 ) this.value = 12;
		J.calendar.predraw( new Date(J.calendar.y, this.value - 1, J.calendar.d) );
	},
	
	predraw : function(d)
	{
		this.draw( d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate() );
	},
	
	setdate : function()
	{
	    var c = J.calendar, d = J(this).html(), time;
		if( c.m < 10 ) c.m = '0' + c.m; if( d < 10 ) d = '0' + d;
		
		time = c.time ? new Date().toLocaleTimeString() : '';
		if( time != '' ) time = time.length < 8 ? ' 0' + time : ' ' + time;
		
		if( c.type == '-')
		    J(c.inu).val( c.y + '-' + c.m + '-' + d + time );
		else
		    J(c.inu).val( c.m + '/' + d + '/' + c.y + time ); c._p.hide( this, c._p );
	},
	
	over : function()
	{
		if( this.className != 'tday' ) J(this).acls('over');
		
		var c = J.calendar, d = J(this).html();
		J('t2',c._p._doc).html( c.y + '年' + c.m + '月' + d + '日' );
	},
	
	out : function()
	{
	    if( this.className != 'tday' ) J(this).acls('tdday');
		J('t2',J.calendar._p._doc).html( new Date().toLocaleDateString() );
	},
	
	year : function()
	{
		J(this).html('<input id="iy" type="text" value="' + J.calendar.y + '" style="width:32px;">');
		var obj = J.calendar._p; J('#iy',obj._doc).focus(); J('iy',obj._doc).blur(J.calendar.cy);
		J('iy',obj._doc).click(function(e){
		    var ev = e || obj._ifrm.contentWindow.event;
			ev.stopPropagation ? ev.stopPropagation() : (ev.cancelBubble = true);
		});
	},
	
	month : function()
	{
	    J(this).html('<input id="im" type="text" value="' + J.calendar.m + '" style="width:20px;">');
		var obj = J.calendar._p; J('#im',obj._doc).focus(); J('im',obj._doc).blur(J.calendar.cm);
		J('im',obj._doc).click(function(e){
		    var ev = e || obj._ifrm.contentWindow.event;
			ev.stopPropagation ? ev.stopPropagation() : (ev.cancelBubble = true);
		});
	},
	
	c_clean : function()
	{
	    this.type = null; this.than = null; this.time = null; this.inu = null; this._p = null;
	}
});

J(window).aevt( 'load', function(){ J.calendar = new calendar(J.califrm); } );