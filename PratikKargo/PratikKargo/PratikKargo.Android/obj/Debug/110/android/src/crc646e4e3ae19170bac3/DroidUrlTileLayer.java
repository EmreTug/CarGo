package crc646e4e3ae19170bac3;


public class DroidUrlTileLayer
	extends com.google.android.gms.maps.model.UrlTileProvider
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getTileUrl:(III)Ljava/net/URL;:GetGetTileUrl_IIIHandler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.GoogleMaps.Android.DroidUrlTileLayer, Xamarin.Forms.GoogleMaps.Android", DroidUrlTileLayer.class, __md_methods);
	}


	public DroidUrlTileLayer (int p0, int p1)
	{
		super (p0, p1);
		if (getClass () == DroidUrlTileLayer.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.GoogleMaps.Android.DroidUrlTileLayer, Xamarin.Forms.GoogleMaps.Android", "System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public java.net.URL getTileUrl (int p0, int p1, int p2)
	{
		return n_getTileUrl (p0, p1, p2);
	}

	private native java.net.URL n_getTileUrl (int p0, int p1, int p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
