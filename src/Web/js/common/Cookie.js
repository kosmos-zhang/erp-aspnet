var Cookie={
  set:function(name,value,min)   
  {         
      var   exp     =   new   Date();       
      exp.setTime(exp.getTime()   +   min*60*1000);   
      document.cookie   =   name   +   "="+   escape(value)   +";expires="+   exp.toGMTString()+";path=/"; 
            
  },   
  
  get:function(name)   
  {      

      var exp = "(^|[\\s]+)"+name+"=([^;]*)(;|$)";      
      var   arr   =   document.cookie.match(new RegExp(exp)); 
      
      if(arr   !=   null)
         return   unescape(arr[2]); 
      return   null;   
  },
     
  del:function(name)   
  {         
      var   exp   =   new   Date();   
      exp.setTime(exp.getTime()   -   1);  
    
      var   cval=this.get(name); 
        
      if(cval!=null)
         document.cookie=name   +"="+cval+";expires="+exp.toGMTString();   
  }
}
