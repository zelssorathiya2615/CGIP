// using UnityEngine;

// public class KeplerSolver
// {
    
// }


using UnityEngine;

public static class KeplerSolver
{
    private const double DegreesToRadians = System.Math.PI / 180.0;
    private const double RadiansToDegrees = 180.0 / System.Math.PI;

    public static Vector3 CalculatePosition(
        PlanetData data,
        double julianDate
    )
    {
        // Number of Julian centuries since J2000.0
        double T = (julianDate - 2451545.0) / 36525.0;

        // Calculate the six orbital elements at time T.
        double a =
            data.semiMajorAxisAU +
            data.semiMajorAxisRate * T;

        double e =
            data.eccentricity +
            data.eccentricityRate * T;

        double inclination =
            data.inclinationDeg +
            data.inclinationRate * T;

        double meanLongitude =
            data.meanLongitudeDeg +
            data.meanLongitudeRate * T;

        double longitudeOfPerihelion =
            data.longitudeOfPerihelionDeg +
            data.longitudeOfPerihelionRate * T;

        double longitudeOfAscendingNode =
            data.longitudeOfAscendingNodeDeg +
            data.longitudeOfAscendingNodeRate * T;

        // Argument of perihelion.
        double omega =
            longitudeOfPerihelion -
            longitudeOfAscendingNode;

        // Mean anomaly.
        double M =
            meanLongitude -
            longitudeOfPerihelion;

        // Additional JPL correction terms.
        M +=
            data.b * T * T +
            data.c * System.Math.Cos(data.f * T * DegreesToRadians) +
            data.s * System.Math.Sin(data.f * T * DegreesToRadians);

        // Normalize M to 0-360 degrees.
        M = NormalizeDegrees(M);

        // Solve Kepler's equation.
        double E = SolveKeplersEquation(M, e);

        // Convert eccentric anomaly to true anomaly.
        double trueAnomaly = CalculateTrueAnomaly(E, e);

        // Distance from the Sun.
        double distance =
            a * (1.0 - e * System.Math.Cos(E));

        // Convert orbital elements to Cartesian coordinates.
        double omegaRad = omega * DegreesToRadians;
        double nodeRad =
            longitudeOfAscendingNode * DegreesToRadians;
        double inclinationRad =
            inclination * DegreesToRadians;

        double argument = omegaRad + trueAnomaly;

        double x =
            distance *
            (
                System.Math.Cos(nodeRad) *
                System.Math.Cos(argument)
                -
                System.Math.Sin(nodeRad) *
                System.Math.Sin(argument) *
                System.Math.Cos(inclinationRad)
            );

        double y =
            distance *
            (
                System.Math.Sin(nodeRad) *
                System.Math.Cos(argument)
                +
                System.Math.Cos(nodeRad) *
                System.Math.Sin(argument) *
                System.Math.Cos(inclinationRad)
            );

        double z =
            distance *
            (
                System.Math.Sin(argument) *
                System.Math.Sin(inclinationRad)
            );

        // Convert from AU to Unity units.
        const double AU_TO_UNITY = 20.0;

        return new Vector3(
            (float)(x * AU_TO_UNITY),
            (float)(z * AU_TO_UNITY),
            (float)(y * AU_TO_UNITY)
        );
    }

// 


public static Vector3 CalculateOrbitPoint(
    PlanetData data,
    double julianDate,
    double trueAnomalyRadians
)
{
    double T = (julianDate - 2451545.0) / 36525.0;

    // Time-adjusted orbital elements.
    double a =
        data.semiMajorAxisAU +
        data.semiMajorAxisRate * T;

    double e =
        data.eccentricity +
        data.eccentricityRate * T;

    double inclination =
        data.inclinationDeg +
        data.inclinationRate * T;

    double longitudeOfPerihelion =
        data.longitudeOfPerihelionDeg +
        data.longitudeOfPerihelionRate * T;

    double longitudeOfAscendingNode =
        data.longitudeOfAscendingNodeDeg +
        data.longitudeOfAscendingNodeRate * T;

    // Argument of perihelion.
    double omega =
        longitudeOfPerihelion -
        longitudeOfAscendingNode;

    double omegaRad = omega * DegreesToRadians;

    double nodeRad =
        longitudeOfAscendingNode * DegreesToRadians;

    double inclinationRad =
        inclination * DegreesToRadians;

    // Elliptical orbit equation.
    double distance =
        a * (1.0 - e * e) /
        (1.0 + e * System.Math.Cos(trueAnomalyRadians));

    double argument =
        omegaRad + trueAnomalyRadians;

    double x =
        distance *
        (
            System.Math.Cos(nodeRad) *
            System.Math.Cos(argument)
            -
            System.Math.Sin(nodeRad) *
            System.Math.Sin(argument) *
            System.Math.Cos(inclinationRad)
        );

    double y =
        distance *
        (
            System.Math.Sin(nodeRad) *
            System.Math.Cos(argument)
            +
            System.Math.Cos(nodeRad) *
            System.Math.Sin(argument) *
            System.Math.Cos(inclinationRad)
        );

    double z =
        distance *
        System.Math.Sin(argument) *
        System.Math.Sin(inclinationRad);

    const double AU_TO_UNITY = 20.0;

    // Same coordinate mapping as CalculatePosition().
    return new Vector3(
        (float)(x * AU_TO_UNITY),
        (float)(z * AU_TO_UNITY),
        (float)(y * AU_TO_UNITY)
    );
}











// 


    private static double SolveKeplersEquation(
        double meanAnomalyDegrees,
        double eccentricity
    )
    {
        double M =
            meanAnomalyDegrees * DegreesToRadians;

        double E =
            (eccentricity < 0.8)
                ? M
                : System.Math.PI;

        for (int i = 0; i < 20; i++)
        {
            double delta =
                (E - eccentricity * System.Math.Sin(E) - M)
                /
                (1.0 - eccentricity * System.Math.Cos(E));

            E -= delta;

            if (System.Math.Abs(delta) <= 1e-10)
                break;
        }

        return E;
    }

    private static double CalculateTrueAnomaly(
        double eccentricAnomaly,
        double eccentricity
    )
    {
        double sinHalf =
            System.Math.Sqrt(1.0 + eccentricity) *
            System.Math.Sin(eccentricAnomaly / 2.0);

        double cosHalf =
            System.Math.Sqrt(1.0 - eccentricity) *
            System.Math.Cos(eccentricAnomaly / 2.0);

        return 2.0 *
               System.Math.Atan2(sinHalf, cosHalf);
    }

    private static double NormalizeDegrees(double angle)
    {
        angle %= 360.0;

        if (angle < 0)
            angle += 360.0;

        return angle;
    }
}