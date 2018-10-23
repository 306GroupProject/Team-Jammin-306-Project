using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decisionTree  {

	// delegates:

	public delegate bool Decision();
	public delegate void Action(); 



	Action action; // I can create varaibles for each of my Delegates.
	Decision decision; 
	decisionTree  leftNode; 
	decisionTree  rightNode;

	public decisionTree(){

		this.action = null;
		this.decision = null; 
		this.leftNode = null;
		this.rightNode = null;

	}

	/**
	 * 
	 * 
	 * 
	 */ 

	public  void buildDecision(Decision aiChoice){

		this.decision = aiChoice; 

	
	}

	public  void buildAction(Action aiAction){

		this.action = aiAction; 

	}

	public void Left(decisionTree leftNode){


		this.leftNode = leftNode; 

	}


	public void Right(decisionTree rightNode){

		this.rightNode = rightNode; 
	}

	/**
	 * recusvily search throughout my tree until find the correct action to take. 
	 */ 
	public void search(){

		if (action != null) {
				
			action ();


		} else if (this.decision()) {

			rightNode.search (); 

		} else {
			leftNode.search(); 
		}


	}


}
