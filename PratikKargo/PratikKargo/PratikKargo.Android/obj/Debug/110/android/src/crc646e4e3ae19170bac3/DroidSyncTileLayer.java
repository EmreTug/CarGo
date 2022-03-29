package crc646e4e3ae19170bac3;


public class DroidSyncTileLayer
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.model.TileProvider
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getTile:(III)Lcom/google/android/gms/maps/model/Tile;:GetGetTile_IIIHandler:Android.Gms.Maps.Model.ITileProviderInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.GoogleMaps.Android.DroidSyncTileLayer, Xamarin.Forms.GoogleMaps.Android", DroidSyncTileLayer.class, __md_methods);
	}


	public DroidSyncTileLayer ()
	{
		super ();
		if (getClass () == DroidSyncTileLayer.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.GoogleMaps.Android.DroidSyncTileLayer, Xamarin.Forms.GoogleMaps.Android", "", this, new java.lang.Object[] {  });
	}


	public com.google.android.gms.maps.model.Tile getTile (int p0, int p1, int p2)
	{
		return n_getTile (p0, p1, p2);
	}

	private native com.google.android.gms.maps.model.Tile n_getTile (int p0, int p1, int p2);

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
