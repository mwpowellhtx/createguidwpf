# GUIDGEN (WPF)

If you have been around the Microsoft development stacks for any length of time, especially
hearkening back to the good old days of ActiveX, then you are undoubtedly familiar with the
stand by utility, GUIDGEN. Typically you can find it lurking in the bowels of your favorite
version of Visual Studio, I believe it also distributes with the Platform SDKs, and so on.

Well, recently , I found that I wanted to generate some GUIDs. Do not ask me why. I just did.
And I needed a version of GUIDGEN to support a couple of different flavors that were not there
in the traditional version. So I thought to myself: "Self, why not work up a 'simple' WPF MVVM
style version, along similar lines with the GUIDGEN," and thus was borne GUIDGENWPF.

Internally, it's quite simple. The crux of the matter is to take a System.Guid, along with some
nominal options (such as text case, upper or lower, etc), and generate the desired Guid. Same as
with the traditional version, copy that to the Clipboard. No problem.

MVVM enters the picture in the sense that it also leverages model binding goodness, even two-way
binding for selecting the desired format. Data bound results are displayed upon selection. Really
quite simple, and demonstrates how a ListView may be bound to a View Model, and how that gets
automatically updated.

There are a million different ways those details could be injected into the generator view models,
but for simplicity sake, I am keeping it simple for now. I think it serves as a pretty good example
of some fairly introductory concepts of the WPF and MVVM architectures.

Feedback, comments, etc are welcome and appreciated.

Thank you.
