/*
 * FckEditor导入Word插件 ASP.net版
 * 作者 科飞软件 申国新 2009.4.23
 * 本插件脚本部分借鉴了placeholder插件的规范格式，对其作者表示感谢。
 */

// Register
FCKCommands.RegisterCommand( 'ImportWord', new FCKDialogCommand( 'ImportWord', FCKLang.ImportWordDlgTitle, FCKPlugins.Items['ImportWord'].Path + 'ImportWord.aspx', 350, 250 ) ) ;

// Create the "Word" toolbar button.
var oImportWordItem = new FCKToolbarButton( 'ImportWord', FCKLang.ImportWordBtn ) ;
oImportWordItem.IconPath = FCKPlugins.Items['ImportWord'].Path + 'ImportWord.gif' ;

FCKToolbarItems.RegisterItem( 'ImportWord', oImportWordItem ) ;


// The object used for all ImportWord operations.
var FCKImportWords = new Object() ;


/*
// Add a new ImportWord at the actual selection.
FCKImportWords.Add = function( name )
{
	var oSpan = FCK.InsertElement( 'span' ) ;
	this.SetupSpan( oSpan, name ) ;
}

FCKImportWords.SetupSpan = function( span, name )
{
	span.innerHTML = '[[ ' + name + ' ]]' ;

	span.style.backgroundColor = '#ffff00' ;
	span.style.color = '#000000' ;

	if ( FCKBrowserInfo.IsGecko )
		span.style.cursor = 'default' ;

	span._fckImportWord = name ;
	span.contentEditable = false ;

	// To avoid it to be resized.
	span.onresizestart = function()
	{
		FCK.EditorWindow.event.returnValue = false ;
		return false ;
	}
}

// On Gecko we must do this trick so the user select all the SPAN when clicking on it.
FCKImportWords._SetupClickListener = function()
{
	FCKImportWords._ClickListener = function( e )
	{
		if ( e.target.tagName == 'SPAN' && e.target._fckImportWord )
			FCKSelection.SelectNode( e.target ) ;
	}

	FCK.EditorDocument.addEventListener( 'click', FCKImportWords._ClickListener, true ) ;
}

// Open the ImportWord dialog on double click.
FCKImportWords.OnDoubleClick = function( span )
{
	if ( span.tagName == 'SPAN' && span._fckImportWord )
		FCKCommands.GetCommand( 'ImportWord' ).Execute() ;
}

FCK.RegisterDoubleClickHandler( FCKImportWords.OnDoubleClick, 'SPAN' ) ;

// Check if a Placholder name is already in use.
FCKImportWords.Exist = function( name )
{
	var aSpans = FCK.EditorDocument.getElementsByTagName( 'SPAN' ) ;

	for ( var i = 0 ; i < aSpans.length ; i++ )
	{
		if ( aSpans[i]._fckImportWord == name )
			return true ;
	}

	return false ;
}

if ( FCKBrowserInfo.IsIE )
{
	FCKImportWords.Redraw = function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return ;

		var aPlaholders = FCK.EditorDocument.body.innerText.match( /\[\[[^\[\]]+\]\]/g ) ;
		if ( !aPlaholders )
			return ;

		var oRange = FCK.EditorDocument.body.createTextRange() ;

		for ( var i = 0 ; i < aPlaholders.length ; i++ )
		{
			if ( oRange.findText( aPlaholders[i] ) )
			{
				var sName = aPlaholders[i].match( /\[\[\s*([^\]]*?)\s*\]\]/ )[1] ;
				oRange.pasteHTML( '<span style="color: #000000; background-color: #ffff00" contenteditable="false" _fckImportWord="' + sName + '">' + aPlaholders[i] + '</span>' ) ;
			}
		}
	}
}
else
{
	FCKImportWords.Redraw = function()
	{
		if ( FCK.EditMode != FCK_EDITMODE_WYSIWYG )
			return ;

		var oInteractor = FCK.EditorDocument.createTreeWalker( FCK.EditorDocument.body, NodeFilter.SHOW_TEXT, FCKImportWords._AcceptNode, true ) ;

		var	aNodes = new Array() ;

		while ( ( oNode = oInteractor.nextNode() ) )
		{
			aNodes[ aNodes.length ] = oNode ;
		}

		for ( var n = 0 ; n < aNodes.length ; n++ )
		{
			var aPieces = aNodes[n].nodeValue.split( /(\[\[[^\[\]]+\]\])/g ) ;

			for ( var i = 0 ; i < aPieces.length ; i++ )
			{
				if ( aPieces[i].length > 0 )
				{
					if ( aPieces[i].indexOf( '[[' ) == 0 )
					{
						var sName = aPieces[i].match( /\[\[\s*([^\]]*?)\s*\]\]/ )[1] ;

						var oSpan = FCK.EditorDocument.createElement( 'span' ) ;
						FCKImportWords.SetupSpan( oSpan, sName ) ;

						aNodes[n].parentNode.insertBefore( oSpan, aNodes[n] ) ;
					}
					else
						aNodes[n].parentNode.insertBefore( FCK.EditorDocument.createTextNode( aPieces[i] ) , aNodes[n] ) ;
				}
			}

			aNodes[n].parentNode.removeChild( aNodes[n] ) ;
		}

		FCKImportWords._SetupClickListener() ;
	}

	FCKImportWords._AcceptNode = function( node )
	{
		if ( /\[\[[^\[\]]+\]\]/.test( node.nodeValue ) )
			return NodeFilter.FILTER_ACCEPT ;
		else
			return NodeFilter.FILTER_SKIP ;
	}
}

FCK.Events.AttachEvent( 'OnAfterSetHTML', FCKImportWords.Redraw ) ;

// We must process the SPAN tags to replace then with the real resulting value of the ImportWord.
FCKXHtml.TagProcessors['span'] = function( node, htmlNode )
{
	if ( htmlNode._fckImportWord )
		node = FCKXHtml.XML.createTextNode( '[[' + htmlNode._fckImportWord + ']]' ) ;
	else
		FCKXHtml._AppendChildNodes( node, htmlNode, false ) ;

	return node ;
}*/