CsPacman
========

About
-----

This is a C# port of [JPacman-Framework](https://github.com/SERG-Delft/jpacman-framework) 
by Arie van Deursen and his team of Delft University of Technology.

The original JPacman-Framework (which is written in Java) is the running example in the 
Java edition of the book
_Building Maintainable Software: Ten Guidelines for Future-Proof Code_, by Joost Visser,
Pascal van Eck, Rob van der Leek, Sylvan Rigal and Gijs Wijnholds (published by O'Reilly
Media, ISBN: 978-1-4919-5352-5).

This C# port is used as the running example in the C# edition of the same title, to be
published in May 2016. 

Main contributors of the Java version:

*	Arie van Deursen (versions 1.0-5.x, 2003-2013)
*	Jeroen Roosen (major rewrite, version 6.0, 2014)

C# port by Pascal van Eck.

Porting notes
-------------

The original JPacman-framework uses AWT as UI toolkit. The port is for Windows Forms, as
this is the .NET framework closest to AWT (even though Windows Forms is nowadays replaced by
more modern frameworks).

All Javadoc comments by the original authors have been removed; they are not ported to 
XMLDOC.

Like in the original Java JPacman-framework, parts of the code are left untested intentionally. 

The port stays as close as possible to the original Java version, with the following exceptions:
* Code is formatted according to the C# / Visual Studio tradition.
* Wherever possible, (auto-)properties are introduced (which do not exist in Java) to give
a C# flavour to the code.
* Method names are converted to PascalCase, also to give a C# flavour to the code.

The unit tests require NUnit and Moq. When using Visual Studio, NUnit in turn requires the
NUnit Visual Studio Adapter.
