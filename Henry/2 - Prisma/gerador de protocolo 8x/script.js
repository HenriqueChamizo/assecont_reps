var hex = "0123456789ABCDEF";
var almostAscii = ' !"#$%&' + "'" + '()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[' + '\\' + ']^_`abcdefghijklmnopqrstuvwxyz{|}';

function generate()
{
	var textArea = document.getElementById('result');
	var inputArea = document.getElementById('input');
	textArea.value = "02 ";
	textArea.value += generateParamLength(inputArea);
	textArea.value += DoAsciiHex(inputArea.value, "A2H");
	var auxInputArea = inputArea;
	textArea.value += checkSum(inputArea);
	textArea.value += " 03 ";
}

function checkSum(inputArea)
{
	var i;
	var check = 0;

	var paramLength = inputArea.value.length;

	for (i = 0; i < paramLength; i++)
	{
		var textPos = inputArea.value.charAt(i);
		var val = almostAscii.indexOf(textPos) + 32;
		check ^= val;
	}

	check ^= paramLength % 256;
	check ^= paramLength / 256;

	var h16 = Math.floor(check / 16);
	var h1 = check % 16;
	return (hex.charAt(h16) + hex.charAt(h1));
	
}

function generateParamLength(inputArea)
{
	var paramLength = inputArea.value.length;
	var h1 = paramLength % 256;
	var h16 = Math.floor(paramLength / 256);

	var h1Str = h1.toString(16);
	if (h1Str.length == 1)
	{
		h1Str = '0' + h1Str;
	}
	var h16Str = h16.toString(16);
	if (h16Str.length == 1)
	{
		h16Str = '0' + h16Str;
	}

	var tempStr = h1Str + ' ' + h16Str + ' ';	
	return tempStr.toUpperCase();
}

function DoAsciiHex(x, dir)
{
	var r = "";
	if (dir == "A2H")
	{
		for (i=0;i<x.length;i++)
		{
			var let = x.charAt(i);
			var pos = almostAscii.indexOf(let) + 32;
			var h16 = Math.floor(pos/16);
			var h1 = pos%16;
			r+=hex.charAt(h16)+hex.charAt(h1) + ' ';
		}
	}

	if (dir == "H2A")
	{
		for(i=0;i<x.length;i++)
		{
			let1=x.charAt(2*i);
			let2=x.charAt(2*i+1);
			val=hex.indexOf(let1)*16+hex.indexOf(let2);
			r+=almostAscii.charAt(val-32);
		}
	}

	return r;
}