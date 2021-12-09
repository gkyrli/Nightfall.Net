using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class DetectionRule
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("logicalOp")]
        public string LogicalOp { get; }

        [JsonPropertyName("detectors")]
        public ICollection<Detector> Detectors { get; }

        private DetectionRule(string name, LogicalOperation logicalOp, ICollection<Detector> detectors = null)
        {
            Name = name;
            LogicalOp = logicalOp.ToString();
            Detectors = detectors ?? new HashSet<Detector>();
        }

        public static DetectionRule GetANYDetectionRule(string name, ICollection<Detector> detectors = null)
        {
            return new DetectionRule(name, LogicalOperation.ANY, detectors);
        }

        public static DetectionRule GetALLDetectionRule(string name)
        {
            return new DetectionRule(name, LogicalOperation.ALL);
        }

        public void AddUUidDetector(string uuid,int minNumFindings=1,ConfidenceEnum minConfidence=ConfidenceEnum.POSSIBLE )
        {
            Detectors.Add(new Detector(){DetectorUUID = uuid,MinNumFindings = minNumFindings,MinConfidence = minConfidence.ToString()});
        }
        public void AddDetectors(params Detector[] detectors)
        {
            foreach (var detector in detectors)
            {
                Detectors.Add(detector);
            }
        }

    }
}