using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// namespace Scripts.Systems
// {
	public class ChargeSystem : MonoBehaviour
	{
		public event Action <ChargeSystem> OnCharageGained;
		public event Action <ChargeSystem> OnChargeMaxed;

		public int currentCharge = 0;
		public int maxCharage;

		public void Start()
		{
			currentCharge = 0;
		}

		public ChargeSystem(int max)
		{
			this.maxCharage = max;
		}

		public void IncreaseCharge(int amount)
		{
			if (IsChargeFull())
			{
				return;
			}
			if (currentCharge < maxCharage)
			{
				currentCharge += amount;
				this.OnCharageGained?.Invoke(this);

				if (currentCharge >= maxCharage)
				{
					this.OnChargeMaxed.Invoke(this);
				}
			}
		}

		public bool IsChargeFull()
        {
			return currentCharge >= maxCharage;
		}

		public void ResetCharge()
		{
			currentCharge = 0;
		}

	}
// }
