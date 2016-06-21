using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpNeat.Core;
using System.Collections;
using UnityEngine;

namespace SharpNEAT.Core
{
    class UnityParallelListEvaluator<TGenome, TPhenome> : IGenomeListEvaluator<TGenome>
        where TGenome : class, IGenome<TGenome>
        where TPhenome : class
    {

        readonly IGenomeDecoder<TGenome, TPhenome> _genomeDecoder;
        IPhenomeEvaluator<TPhenome> _phenomeEvaluator;
        //readonly IPhenomeEvaluator<TPhenome> _phenomeEvaluator;
        Optimizer _optimizer;

        #region Constructor

        /// <summary>
        /// Construct with the provided IGenomeDecoder and IPhenomeEvaluator.
        /// </summary>
        public UnityParallelListEvaluator(IGenomeDecoder<TGenome, TPhenome> genomeDecoder,
                                         IPhenomeEvaluator<TPhenome> phenomeEvaluator,
                                          Optimizer opt)
        {
            _genomeDecoder = genomeDecoder;
            _phenomeEvaluator = phenomeEvaluator;
            _optimizer = opt;
        }

        #endregion

        public ulong EvaluationCount
        {
            get { return _phenomeEvaluator.EvaluationCount; }
        }

        public bool StopConditionSatisfied
        {
            get { return _phenomeEvaluator.StopConditionSatisfied; }
        }

        public IEnumerator Evaluate(IList<TGenome> genomeList)
        {
            yield return Coroutiner.StartCoroutine(evaluateList(genomeList));
        }

        private IEnumerator evaluateList(IList<TGenome> genomeList)
        {
			GameObject goal = GameObject.Find ("Goal");
//			GameObject iter1, iter2, iter3, iter4, iter5;
//			GameObject dist1, dist2, dist3, dist4, dist5;
//
//			iter1 = GameObject.Find ("Iteration 1");
//			iter2 = GameObject.Find ("Iteration 2");
//			iter3 = GameObject.Find ("Iteration 3");
//			iter4 = GameObject.Find ("Iteration 4");
//			iter5 = GameObject.Find ("Iteration 5");
//            
//			dist1 = GameObject.Find ("Distances 1");
//			dist2 = GameObject.Find ("Distances 2");
//			dist3 = GameObject.Find ("Distances 3");
//			dist4 = GameObject.Find ("Distances 4");
//			dist5 = GameObject.Find ("Distances 5");

			Dictionary<TGenome, TPhenome> dict = new Dictionary<TGenome, TPhenome>();
            Dictionary<TGenome, FitnessInfo[]> fitnessDict = new Dictionary<TGenome, FitnessInfo[]>();
            for (int i = 0; i < _optimizer.Trials; i++)
            {
				/*
				 * This section controls the trial Maze locations
				 * 
				 * */
				switch (i) {
				case 0:
					goal.transform.position = new Vector3 (2.59f, -3.53f, 0f);

//					dist1.SetActive (true);
//					dist2.SetActive (false);
//					dist3.SetActive (false);
//					dist4.SetActive (false);
//					dist5.SetActive (false);
//
//					iter2.SetActive (false);
//					iter3.SetActive (false);
//					iter4.SetActive (false);
//					iter5.SetActive (false);
//					iter1.SetActive (true);
					break;
				case 1:
					goal.transform.position = new Vector3 (-1.23f, 0f, 0f);
//					iter1.SetActive (false);
//					iter2.SetActive (true);
//
//					dist1.SetActive (false);
//					dist2.SetActive (true);

					break;
				case 2:
					goal.transform.position = new Vector3 (6.55f, -3.2f, 0f);
//					iter2.SetActive (false);
//					iter3.SetActive (true);
//
//					dist2.SetActive (false);
//					dist3.SetActive (true);
					break;
				case 3:
					goal.transform.position = new Vector3 (-4.82f, -2f, 0f);
//					iter3.SetActive(false);
//					iter4.SetActive(true);
//
//					dist3.SetActive (false);
//					dist4.SetActive (true);
					break;
				case 4:
					goal.transform.position = new Vector3 (8f, 2.7f, 0f);
//					iter4.SetActive(false);
//					iter5.SetActive(true);
//
//					dist4.SetActive (false);
//					dist5.SetActive (true);
					break;
				case 5:
					goal.transform.position = new Vector3 (2.65f, -3.71f, 0f);
					break;

				}
                Utility.Log("Iteration " + (i + 1));                
                _phenomeEvaluator.Reset();
                dict = new Dictionary<TGenome, TPhenome>();
                foreach (TGenome genome in genomeList)
                {
                    
                    TPhenome phenome = _genomeDecoder.Decode(genome);
                    if (null == phenome)
                    {   // Non-viable genome.
                        genome.EvaluationInfo.SetFitness(0.0);
                        genome.EvaluationInfo.AuxFitnessArr = null;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            fitnessDict.Add(genome, new FitnessInfo[_optimizer.Trials]);
                        }
                        dict.Add(genome, phenome);
                        //if (!dict.ContainsKey(genome))
                        //{
                        //    dict.Add(genome, phenome);
                        //    fitnessDict.Add(phenome, new FitnessInfo[_optimizer.Trials]);
                        //}
                        Coroutiner.StartCoroutine(_phenomeEvaluator.Evaluate(phenome));


                    }
                }

                yield return new WaitForSeconds(_optimizer.TrialDuration);
                
                foreach (TGenome genome in dict.Keys)
                {
                    TPhenome phenome = dict[genome];
                    if (phenome != null)
                    {

                        FitnessInfo fitnessInfo = _phenomeEvaluator.GetLastFitness(phenome);
                        
                        fitnessDict[genome][i] = fitnessInfo;
                    }
                }
            }
            foreach (TGenome genome in dict.Keys)
            {
                TPhenome phenome = dict[genome];
                if (phenome != null)
                {
                    double fitness = 0;

                    for (int i = 0; i < _optimizer.Trials; i++)
                    {
                     
                        fitness += fitnessDict[genome][i]._fitness;
                       
                    }
                    var fit = fitness;
                    fitness /= _optimizer.Trials; // Averaged fitness
                    
                    if (fit > _optimizer.StoppingFitness)
                    {
                      //  Utility.Log("Fitness is " + fit + ", stopping now because stopping fitness is " + _optimizer.StoppingFitness);
                      //  _phenomeEvaluator.StopConditionSatisfied = true;
                    }
                    genome.EvaluationInfo.SetFitness(fitness);
                    genome.EvaluationInfo.AuxFitnessArr = fitnessDict[genome][0]._auxFitnessArr;
                }
            }

//			iter2.SetActive (true);
//			iter3.SetActive (true);
//			iter4.SetActive (true);
//			iter5.SetActive (true);
//			iter1.SetActive (true);
//
//			dist1.SetActive (true);
//			dist2.SetActive (true);
//			dist3.SetActive (true);
//			dist4.SetActive (true);
//			dist5.SetActive (true);
        }

        public void Reset()
        {
            _phenomeEvaluator.Reset();
        }
    }
}
