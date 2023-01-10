using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatorAND : Validator {

    Validator[] statements;

	public ValidatorAND(Validator[] statements) {

		this.statements = statements;
	}

	public override bool IsValid() {

		bool result = true;

		foreach (Validator statement in statements)
			result = result && statement.IsValid();

		return result;
	}
}
