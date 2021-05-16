using Godot;
using System;

namespace Helpers{
public class MeshHelper : Node
{
    public void MakeScenesWork(Spatial _ReferenceScene, int RemovedMask, int AddedMask)
  {
		for(int x = 0; x < _ReferenceScene.GetChildCount();x++)
		{     
			if(_ReferenceScene.GetChild(x) is MeshInstance)
			{
			MeshInstance hek = (MeshInstance)_ReferenceScene.GetChild(x);
			hek.SetLayerMaskBit(RemovedMask,false);
			hek.SetLayerMaskBit(AddedMask,true);
			}
		}
  }
}
}