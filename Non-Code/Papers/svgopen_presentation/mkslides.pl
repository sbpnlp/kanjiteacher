#!/usr/bin/perl

use strict;
use warnings;

$/ = "\n\n";
my @slides = <DATA>;
my $list = "    <ol>\n";
for (0 .. $#slides) {
	my $i = $_ + 1;
	$_ = $slides[$_];
	my $prev = $i == 1 ? "index.html" : "slide-" . ($i - 1) . ".html";
	my $next = $i == @slides ? "index.html" : "slide-" . ($i + 1) . ".html";
	/\n/;
	my ($title, $svg) = ($`, $');
	$title =~ s/\s+/ /g;
	$title =~ s/^ //;
	$title =~ s/ $//;
	$svg =~ s/\s+/ /g;
	$svg =~ s/^ //;
	$svg =~ s/ $//;
	print "slide-$i.html: $title (\"$svg\")\n";
	$list .= "      <li><a href=\"slide-$i.html\">$title</a></li>\n";
	open SLIDE, ">slide-$i.html" or die "Cannot open slide: $!\n";
	print SLIDE <<HTML;
<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
  "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
		<title>$title</title>
		<style type="text/css">
			body { font-family: futura, sans-serif; font-size:24pt }
			.center { text-align: center }
			img { border:0 }
			#topbar { width: 100% }
			#clear { clear: both; margin-top: 2em }
		</style>
	</head>
	<body>
		<p id="topbar">
			<a href="$prev">
				<img src="left.png" alt="&lt;&lt;"
					style="float:left;margin-right:2em"/>
			</a>
			$title
			<a href="$next">
				<img src="right.png" alt="&lt;&lt;"
					style="float:right;margin-left:2em"/>
			</a>
		</p>
		<div id="clear"/>
		<p class="center">
		<object type="image/svg+xml" data="$svg" width="1024"
			height="600">$svg</object>
		</p>
	</body>
</html>
HTML
	close SLIDE;
}
$list .= "    </ol>\n";

open SLIDE, ">index.html" or die "Cannot open slide index.html: $!\n";
print SLIDE <<HTML;
<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN"
  "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
		<title>Index</title>
		<style type="text/css">
			body { font-family: futura, sans-serif }
			.center { text-align: center }
			img { border:0 }
			#topbar { width: 100% }
			#clear { clear: both; margin-top: 2em }
		</style>
	</head>
	<body>
$list
	</body>
</html>
HTML

__DATA__
Building a Graphetic Dictionary for Japanese Kanji
foo

Introduction
bar

Building Principles for Kanji
01-building.svg

Graphemes
02-graphemes.svg

Several Stroke Types
03-stroketypes.svg

Kanji Analysis and XML Description
04-kanalyse.svg

Successive Kanji Strokes
05-kanjistrokes.svg

Practice Sheet
06-practicesheet.svg

Highlighting of Kanji Components
07-components.svg

Animation of Kanji Strokes
08-anim.svg

Stroke Weight Change
09-weightchange.svg

Animation with Different Stroke Weights
10-owari.svg
