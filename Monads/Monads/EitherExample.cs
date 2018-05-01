using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{

    /*
         * IsCandidateEligibleForOffer
         * 1. IsEligible -> raw details
         * 2. Tech test
         * 3. HR interview
         * Each check is very very expensive.. and wants to avoid + we need to know why in case of candidate rejection.
         * 
         *Add Recommendation step..
         *Add step for high influence
         * 
     */



    public class Candidate
    {
        public int Id { get; set; }
        public string Mail { get; set; }
    }

    public class Rejection
    {
        public string Reason { get; set; }
    }

    public static class CadidateEligiblityService
    {
        //Go to really heavy query throw external API which cost lots of money.
        //Than validate with our DB 
        public static bool IsCandidateEligible(int candidateId)
        {
            return true;
        }
    }

    public static class CadidateTechTestService
    {
        //Go to really heavy query throw external API which cost lots of money.
        //Than validate with our DB 
        public static double GetCandidateScore(int candidateId)
        {

            return 88.5;
        }
    }

    public static class CadidateHRInterviewService
    {
        //Go to really heavy query throw external API which cost lots of money.
        //Than validate with our DB 
        public static double GetCandidateScore(int candidateId)
        {

            return 50;
        }
    }

    public static class CandidateRecommendationService
    {
        //Go to really heavy query throw external API which cost lots of money.
        //Than validate with our DB 
        public static bool IsCandidateRecommended(int candidateId)
        {

            return true;
        }
    }

   

    class EitherExample
    {

        public static void IsCandidateEligibleForOffer(Candidate candidate)
        {
            CheckCandiateEligiblity(candidate)
                .Bind(CheckCandidateTechTest)
                .Bind(CheckCandidateHRTest)
                    .Right(SendOfferLetter)
                    .Left(SendRejectionLetter);

        }


        public static void IsCandidateEligibleForOffer2(Candidate candidate)
        {
            CheckCandiateEligiblity(candidate)
                .Bind(CheckCandidateTechTest)
                .Bind(CheckCandidateHRTest)
                .Bind(CheckCandiateRecommendation)
                    .Right(SendOfferLetter)
                    .Left(rejection =>
                    {
                        CheckHighInfluence(rejection, candidate)
                        .Right(SendOfferLetter)
                        .Left(SendRejectionLetter);
                    });

        }




        public static Func<Candidate, Either<Rejection, Candidate>> CheckCandiateEligiblity = (Candidate candidate) =>
        {
            var isEligible = CadidateEligiblityService.IsCandidateEligible(candidate.Id);
            // ternary if cant work!
            if (isEligible)
            {
                return candidate;
            }
            return new Rejection { Reason = "is not eligible" };

        };

        public static Func<Candidate, Either<Rejection, Candidate>> CheckCandidateTechTest = (Candidate candidate) =>
        {
            var scoreTest = CadidateTechTestService.GetCandidateScore(candidate.Id);
            // ternary if cant work!
            if (scoreTest > 85)
            {
                return candidate;
            }
            return new Rejection { Reason = "Fail the tech test" };

        };

        public static Func<Candidate, Either<Rejection, Candidate>> CheckCandidateHRTest = (Candidate candidate) =>
        {
            var scoreTest = CadidateHRInterviewService.GetCandidateScore(candidate.Id);
            // ternary if cant work!
            if (scoreTest > 85)
            {
                return candidate;
            }
            return new Rejection { Reason = "Fail the HR test" };

        };

        public static Func<Candidate, Either<Rejection, Candidate>> CheckCandiateRecommendation = (Candidate candidate) =>
        {
            var isRecommended = CandidateRecommendationService.IsCandidateRecommended(candidate.Id);
            // ternary if cant work!
            if (isRecommended)
            {
                return candidate;
            }
            return new Rejection { Reason = "is not Recommended" };

        };

        public static Func<Rejection, Candidate, Either<Rejection, Candidate>> CheckHighInfluence = (Rejection rejection, Candidate candidate) =>
        {
            throw new NotImplementedException();
        };

        public static void SendOfferLetter(Candidate candidate)
        {

        }

        public static void SendRejectionLetter(Rejection rejection)
        {

        }


    }


}
