function initIframe(iframe) {

	var iframe = document.getElementById(iframe);

	try {

		var bHeight = iframe.contentWindow.document.body.scrollHeight;

		var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;

		var height = Math.max(bHeight, dHeight);

		iframe.height = height;

	} catch (ex) { }

}
