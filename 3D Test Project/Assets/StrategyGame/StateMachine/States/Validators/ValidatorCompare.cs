using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ValidatorCompare<T> : Validator {

	T valueA;
	T valueB;
	Operators op;

	public ValidatorCompare(T valueA, T valueB, Operators op) {

		this.valueA = valueA;
		this.valueB = valueB;
		this.op = op;
	}

	public ValidatorCompare(T valueA, T valueB) : this(valueA, valueB, Operators.equal) {}

	public override bool IsValid() {

		bool result = false;
		
		if (IsNumeric()) {

			double A;
			double.TryParse(valueA.ToString(), out A);
			double B;
			double.TryParse(valueB.ToString(), out B);

			switch (op) {
				case Operators.equal:
					result = A == B;
					break;
				case Operators.less:
					result = A < B;
					break;
				case Operators.lessE:
					result = A <= B;
					break;
				case Operators.greater:
					result = A > B;
					break;
				case Operators.greaterE:
					result = A >= B;
					break;
			}
		}
		else {
			return valueA.Equals(valueB);
		}

		return result;
	}

	bool IsNumeric() {

		bool result = valueA.GetType().Equals(valueB.GetType());

		switch (Type.GetTypeCode(valueA.GetType())) {
			case TypeCode.Byte:
			case TypeCode.SByte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
			case TypeCode.Decimal:
			case TypeCode.Double:
			case TypeCode.Single:
				result = result & true;
				break;
			default:
				result = false;
				break;
		}

		return result;
	}
}
