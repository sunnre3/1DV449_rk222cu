app.filter('processTweet', function ($sce) {
	return function (input) {
		// Save the text.
		var original = input;

		return $sce.trustAsHtml(input
			.replace(/([A-Za-z]+:\/\/[A-Za-z0-9-_]+\.[A-Za-z0-9-_:%&~\?\/.=]+)/g, '<a href="$1">$1</a>')
			.replace(/#(\w+)/g, '<a href="https://twitter.com/hashtag/$1?src=hash">#$1</a>')
			.replace(/@(\w+)/g, '<a href="https://twitter.com/$1">@$1</a>'));
	};
});