using UnityEngine;
using System.Collections;

public class BasicEnemy : Enemy {

	//Start
	protected override void Start()
    {
        //basic enemy properties
        HP = 3;
        moveSpeed = 60f;

        base.Start();
	}
	
	//Update
	protected override void Update()
    {
        base.Update();
	}

    //On destroy
    protected override void OnDestroy()
    {
        AIManager.instance.basicEnemies.Remove(this.gameObject);
    }
}
