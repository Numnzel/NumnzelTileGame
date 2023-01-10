using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatorOR : Validator {

	Validator[] statements;

	public ValidatorOR(Validator[] statements) {

		this.statements = statements;
	}

	public override bool IsValid() {

		bool result = false;

		foreach (Validator statement in statements)
			result = result || statement.IsValid();

		return result;
	}
}
