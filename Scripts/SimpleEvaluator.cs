using UnityEngine;
using System.Collections;
using SharpNeat.Core;
using SharpNeat.Phenomes;
using System.Collections.Generic;

public class SimpleEvaluator : IPhenomeEvaluator<IBlackBox> {

	ulong _evalCount;
    bool _stopConditionSatisfied;
    Optimizer optimizer;
    FitnessInfo fitness;
	//GameObject goal;
    Dictionary<IBlackBox, FitnessInfo> dict = new Dictionary<IBlackBox, FitnessInfo>();

    public ulong EvaluationCount
    {
        get { return _evalCount; }
    }

    public bool StopConditionSatisfied
    {
        get { return _stopConditionSatisfied; }
    }

    public SimpleEvaluator(Optimizer se)
    {
        this.optimizer = se;
    }

    public IEnumerator Evaluate(IBlackBox box)
    {
        if (optimizer != null)
        {
//			goal = GameObject.Find ("Goal");
//			Vector3 startPos = goal.transform.position;
//
//			optimizer.Evaluate(box);
//			yield return new WaitForSeconds(5);
//			float fit = optimizer.GetFitness(box);
//
//			goal.transform.position = new Vector3 (1, 4, 0);
//			yield return new WaitForSeconds(5);
//			fit += optimizer.GetFitness(box);
//
//			goal.transform.position = new Vector3 (7, -3, 0);
//			yield return new WaitForSeconds(5);
//
//			optimizer.StopEvaluation(box);
//			fit += optimizer.GetFitness(box);
//
//			fit /= 3;
//
//			goal.transform.position = startPos;
//			FitnessInfo fitness = new FitnessInfo(fit, fit);
//			dict.Add(box, fitness);

            optimizer.Evaluate(box);
            yield return new WaitForSeconds(optimizer.TrialDuration);
            optimizer.StopEvaluation(box);
            float fit = optimizer.GetFitness(box);
           
            FitnessInfo fitness = new FitnessInfo(fit, fit);
            dict.Add(box, fitness);
           
        }
    }

    public void Reset()
    {
        this.fitness = FitnessInfo.Zero;
        dict = new Dictionary<IBlackBox, FitnessInfo>();
    }

    public FitnessInfo GetLastFitness()
    {
        
        return this.fitness;
    }


    public FitnessInfo GetLastFitness(IBlackBox phenome)
    {
        if (dict.ContainsKey(phenome))
        {
            FitnessInfo fit = dict[phenome];
            dict.Remove(phenome);
           
            return fit;
        }
        
        return FitnessInfo.Zero;
    }
}
