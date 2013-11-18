<?php

namespace view;

class HTMLPage {
	/**
	 * Publicly available method for retrieving
	 * a HTML string for a page.
	 * @param  string $title
	 * @param  string $content
	 * @return string HTML
	 */
	public function getPage($title, $content) {
		return '<!DOCTYPE html>
<html>
	<head>
		<title>' . $title . '</title>
		<meta charset="utf-8">
		<link rel="stylesheet" type="text/css" href="stylesheets/reset.css">
		<link rel="stylesheet" type="text/css" href="stylesheets/basic.css">
		<link rel="stylesheet" type="text/css" href="stylesheets/fonts.css">
		<link rel="stylesheet" type="text/css" href="stylesheets/default.css">
	</head>

	<body>
		<div id="wrapper" class="grid-container">

			<header id="main-header">
				<h1>Närproducenter</h1>
			</header>

			<div class="update">
				<a href="?update">Uppdatera</a>	
			</div>

			' . $content . '

		</div>
	</body>
</html>
		';
	}
}