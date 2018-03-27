using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peso : MonoBehaviour {

	public int peso;
	public bool _ativo = true;

	public virtual bool ativo {
		get{
			return _ativo;
		}
	}
}
