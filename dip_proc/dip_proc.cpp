#include "pch.h"
#include "image_lib.h"


extern "C" {
	//===========================================================================
	//
	//===========================================================================
	__declspec(dllexport) void encode(int *f, int w, int h, int *g)
	{
		int i0, j0;
		int *b, wb, hb;

		wb = w / 4;
		hb = h / 4;
		b = new int[wb*hb];

		i0 = w / 4;
		j0 = h / 4;

		block_get(f, w, h, b, wb, hb, i0, j0);
		contrast(b, wb, hb, 1.5);
		copy(f, w, h, g);
		block_put(b, wb, hb, g, w, h, i0, j0);
		//===========================================================================
	}
	__declspec(dllexport) void encode1(int *f, int w, int h, int *g)
	{
		copy(f, w, h, g);
		//===========================================================================
	}
}
