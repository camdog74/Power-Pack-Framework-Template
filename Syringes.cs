using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PPF
{
    #region SyringeObjects

    internal class TemplateSyringe : OldSyringeBehaviour
    {
        public override Type GetPoisonType()
        {
            return typeof(TemplatePoison);
        }
    }
    
    
    internal class LaserEyeExampleSyringe : OldSyringeBehaviour
    {
        public override Type GetPoisonType()
        {
            return typeof(LaserEyeExamplePoison);
        }
    }
    internal class HeadExploderExampleSyringe : OldSyringeBehaviour
    {
        public override Type GetPoisonType()
        {
            return typeof(HeadExploderExamplePoison);
        }
    }

    internal class HealerExampleSyringe : OldSyringeBehaviour
    {
        public override Type GetPoisonType()
        {
            return typeof(HealerExamplePoison);
        }
    }
    internal class ThickSkinExampleSyringe : OldSyringeBehaviour
    {
        public override Type GetPoisonType()
        {
            return typeof(ThickSkinExamplePoison);
        }
    }

    internal class FlyingFistExampleSyringe : OldSyringeBehaviour
    {
        public override Type GetPoisonType()
        {
            return typeof(FlyingFistExamplePoison);
        }
    }
    internal class GrenadierExampleSyringe : OldSyringeBehaviour
    {
        public override Type GetPoisonType()
        {
            return typeof(GrenadierExamplePoison);
        }
    }






    #endregion


    public class TemplatePoison : PoisonSpreadBehaviour
    {
        public LimbBehaviour limb;

        public override float SpreadSpeed
        {
            get
            {
                return 0;
            }
        }

        public override float Lifespan
        {
            get
            {
                return 1;
            }
        }

        public override void Start()
        {
            PhysicalBehaviour physicalBehaviour = this.Limb.PhysicalBehaviour;

        }



    }


    public class LaserEyeExamplePoison : PoisonSpreadBehaviour
    {
        public LimbBehaviour limb;

        public override float SpreadSpeed
        {
            get
            {
                return 0;
            }
        }

        public override float Lifespan
        {
            get
            {
                return 1;
            }
        }

        public override void Start()
        {
            PhysicalBehaviour physicalBehaviour = this.Limb.PhysicalBehaviour;
            foreach (var body in physicalBehaviour.GetComponent<LimbBehaviour>().Person.Limbs)
            {
                if (body.gameObject.name.Contains("Head"))
                    LaserEyesPowerExample.SetUpPower(body, physicalBehaviour.GetComponent<LimbBehaviour>().Person, body.Person.gameObject, Color.green);
            }
        }



    }

    public class HeadExploderExamplePoison : PoisonSpreadBehaviour
    {
        public LimbBehaviour limb;

        public override float SpreadSpeed
        {
            get
            {
                return 0;
            }
        }

        public override float Lifespan
        {
            get
            {
                return 1;
            }
        }

        public override void Start()
        {
            PhysicalBehaviour physicalBehaviour = this.Limb.PhysicalBehaviour;
            foreach (var body in physicalBehaviour.GetComponent<LimbBehaviour>().Person.Limbs)
            {
                if (body.gameObject.name.Contains("Head"))
                    HeadExploderPowerExample.SetUpPower(body, physicalBehaviour.GetComponent<LimbBehaviour>().Person, body.Person.gameObject);
            }
        }



    }

    public class HealerExamplePoison : PoisonSpreadBehaviour
    {
        public LimbBehaviour limb;

        public override float SpreadSpeed
        {
            get
            {
                return 0;
            }
        }

        public override float Lifespan
        {
            get
            {
                return 1;
            }
        }

        public override void Start()
        {
            PhysicalBehaviour physicalBehaviour = this.Limb.PhysicalBehaviour;
            foreach (var body in physicalBehaviour.GetComponent<LimbBehaviour>().Person.Limbs)
            {
                if (body.gameObject.name.Contains("UpperBody"))
                    HealerExamplePower.SetUpPower(body, physicalBehaviour.GetComponent<LimbBehaviour>().Person, body.Person.gameObject);
            }
        }



    }
    public class ThickSkinExamplePoison : PoisonSpreadBehaviour
    {
        public LimbBehaviour limb;

        public override float SpreadSpeed
        {
            get
            {
                return 0;
            }
        }

        public override float Lifespan
        {
            get
            {
                return 1;
            }
        }

        public override void Start()
        {
            PhysicalBehaviour physicalBehaviour = this.Limb.PhysicalBehaviour;
            foreach (var body in physicalBehaviour.GetComponent<LimbBehaviour>().Person.Limbs)
            {
                if (body.gameObject.name.Contains("UpperBody"))
                    ThickSkinPowerExample.SetUpPower(body, physicalBehaviour.GetComponent<LimbBehaviour>().Person, body.Person.gameObject);
            }
        }



    }
    public class FlyingFistExamplePoison : PoisonSpreadBehaviour
    {
        public LimbBehaviour limb;

        public override float SpreadSpeed
        {
            get
            {
                return 0;
            }
        }

        public override float Lifespan
        {
            get
            {
                return 1;
            }
        }

        public override void Start()
        {
            PhysicalBehaviour physicalBehaviour = this.Limb.PhysicalBehaviour;
            foreach (var body in physicalBehaviour.GetComponent<LimbBehaviour>().Person.Limbs)
            {
                if (body.gameObject.name.Contains("LowerArm"))
                    FlyingFistPowerExample.SetUpPower(body, physicalBehaviour.GetComponent<LimbBehaviour>().Person, body.Person.gameObject);
            }
        }



    }
    public class GrenadierExamplePoison : PoisonSpreadBehaviour
    {
        public LimbBehaviour limb;

        public override float SpreadSpeed
        {
            get
            {
                return 0;
            }
        }

        public override float Lifespan
        {
            get
            {
                return 1;
            }
        }

        public override void Start()
        {
            PhysicalBehaviour physicalBehaviour = this.Limb.PhysicalBehaviour;
            foreach (var body in physicalBehaviour.GetComponent<LimbBehaviour>().Person.Limbs)
            {
                if (body.gameObject.name.Contains("LowerArm"))
                    GrenadierPowerExample.SetUpPower(body, physicalBehaviour.GetComponent<LimbBehaviour>().Person, body.Person.gameObject);
            }
        }



    }







}    
