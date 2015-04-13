# GUIDGEN (WPF)

If you have been around the Microsoft development stacks for any length of time, especially
hearkening back to the good old days of ActiveX, then you are undoubtedly familiar with the
stand by utility, [GUIDGEN](http://msdn.microsoft.com/en-us/library/kw069h38.aspx).
Typically you can find it lurking in the bowels of your favorite version of Visual Studio,
I believe it also distributes with the Platform SDKs, and so on.

Well, recently, I found that I wanted to generate some GUIDs. Do not ask me why. I just did.
And I needed a version of GUIDGEN to support a couple of different flavors that were not there
in the traditional version. So I thought to myself: "Self, why not work up a 'simple' WPF MVVM
style version, along similar lines with the GUIDGEN," and thus was borne GUIDGENWPF.

Internally, it's quite simple. The crux of the matter is to take a
[System.Guid](http;//msdn.microsoft.com/en-us/library/system.guid.aspx), along with some nominal
options (such as text case, upper or lower, etc), and generate the desired Guid format. Same as
with the traditional version, copy that to the Clipboard, except adding a few. No problem.

MVVM enters the picture in the sense that it also leverages model binding goodness, even two-way
binding for selecting the desired format. Data bound results are displayed upon selection. Really
quite simple, and demonstrates how a
[ListView](https://msdn.microsoft.com/en-us/library/system.windows.controls.listview.aspx) may be
bound to a View Model, and how that gets automatically updated.

There are a million different ways those details could be injected, plugged into, or what have you,
into the generator view models, but for simplicity sake, I am keeping it simple for now. I think it
serves as a pretty good example of some fairly introductory concepts of the WPF and MVVM architectures.

I even went so far as to download the
[Visual Studio Image Library](http://www.microsoft.com/en-us/download/details.aspx?id=35825) and connect
one of its images to give the app a little better look and feel than the vanilla WPF style. Nothing fancy
there, just an icon, for both the app executable file and the MainWindow.

I finally decided to move forward with the plugin idea. I added an interface,
[IFormat](http://github.com/mwpowellhtx/guidgenwpf/blob/master/src/GuidGen.Wpf/IFormat.cs), that the
formats all implement. Which leaves each format plugin being a fairly simple class. Actually 'stupidly'
so. All that needs to be specified, basically, is a name and a formatter/handler. Oh, and the plugins also
need to be aware of the [IGeneratorOption](http://github.com/mwpowellhtx/guidgenwpf/blob/master/src/GuidGen.Wpf/IGeneratorOptions.cs).

With this approach, it 'created' another opportunity: to sort the instances according to desired order.
This was previously done by declaratively specifying each instantiation. Again, could do this a hundred
different ways. I decided to add a
[DisplayOrderAttribute](http://github.com/mwpowellhtx/guidgenwpf/blob/master/src/GuidGen.Wpf/DisplayOrderAttribute.cs),
including an extension method to make it easier to look up the value from the type.
This worked pretty well.

Feedback, comments, etc are welcome and appreciated.

Thank you.
