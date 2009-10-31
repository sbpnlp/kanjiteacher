﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// vector of doubles with two components (x,y)
    /// </summary>
    /// <author>Steven Poggel</author>
    /// <created>2009-06</created>
    /// <modified>2009-08-31</modified>
    /// <version>1.0</version>
    [Serializable]
    public struct Vector2
        : IComparable, IComparable<Vector2>, IEquatable<Vector2>, IVector
    {

        #region Class Variables

        /// <summary>
        /// The X component of the vector
        /// </summary>
        private double x;

        /// <summary>
        /// The Y component of the vector
        /// </summary>
        private double y;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the Vector2 class accepting two doubles
        /// </summary>
        /// <param name="x">The new x value for the Vector2</param>
        /// <param name="y">The new y value for the Vector2</param>
        /// <implementation>
        /// Uses the mutator properties for the Vector2 components to allow verification of input (if implemented)
        /// This results in the need for pre-initialisation initialisation of the Vector2 components to 0 
        /// Due to the necessity for struct's variables to be set in the constructor before moving control
        /// </implementation>
        public Vector2(double x, double y)
        {
            this.x = 0;
            this.y = 0;

            X = x;
            Y = y;
        }

        /// <summary>
        /// Constructor for the Vector2 class accepting two floats
        /// </summary>
        /// <param name="x">The new x value for the Vector2</param>
        /// <param name="y">The new y value for the Vector2</param>
        /// <implementation>
        /// Uses the mutator properties for the Vector2 components to allow verification of input (if implemented)
        /// This results in the need for pre-initialisation initialisation of the Vector2 components to 0 
        /// Due to the necessity for struct's variables to be set in the constructor before moving control
        /// </implementation>
        public Vector2(float x, float y)
        {
            this.x = 0;
            this.y = 0;

            X = (double)x;
            Y = (double)y;
        }

        /// <summary>
        /// Constructor for the Vector2 class accepting two Points
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <implementation>
        /// Calculates the vector between two Points.
        /// p1 is the starting point, p2 the end point.
        /// </implementation>
        public Vector2(Point p1, Point p2)
        {
            this.x = 0;
            this.y = 0;

            X = p2.X - p1.X;
            Y = p2.Y - p1.Y;
        }


        /// <summary>
        /// Constructor for the Vector2 class from an array
        /// </summary>
        /// <param name="xyz">Array representing the new values for the Vector2</param>
        /// <implementation>
        /// Uses the VectorArray property to avoid validation code duplication 
        /// </implementation>
        public Vector2(double[] xy)
        {
            this.x = 0;
            this.y = 0;

            Array = xy;
        }

        /// <summary>
        /// Constructor for the Vector2 class from another Vector2 object
        /// </summary>
        /// <param name="v1">Vector2 representing the new values for the Vector2</param>
        /// <implementation>
        /// Copies values from Vector2 v1 to this vector, does not hold a reference to object v1 
        /// </implementation>
        public Vector2(Vector2 v1)
        {
            this.x = 0;
            this.y = 0;

            X = v1.X;
            Y = v1.Y;
        }

        #endregion

        #region Accessors & Mutators

        /// <summary>
        /// Property for the x component of the Vector2
        /// </summary>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Property for the y component of the Vector2
        /// </summary>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Property for the magnitude (aka. length or absolute value) of the Vector2
        /// </summary>
        public double Magnitude
        {
            get
            {
                return
                Math.Sqrt(SumComponentSqrs());
            }
            set
            {
                if (value < 0)
                { throw new ArgumentOutOfRangeException("value", value, NEGATIVE_MAGNITUDE); }

                if (this == new Vector2(0, 0))
                { throw new ArgumentException(ORIGIN_VECTOR_MAGNITUDE, "this"); }

                this = this * (value / Magnitude);
            }
        }

        /// <summary>
        /// Property for the Vector2 as an array
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Thrown if the array argument does not contain exactly three components 
        /// </exception> 
        [XmlIgnore]
        public double[] Array
        {
            get { return new double[] { x, y }; }
            set
            {
                if (value.Length == 2)
                {
                    x = value[0];
                    y = value[1];
                }
                else
                {
                    throw new ArgumentException(TWO_COMPONENTS);
                }
            }
        }

        /// <summary>
        /// An index accessor 
        /// Mapping index [0] -> X and [1] -> Y.
        /// </summary>
        /// <param name="index">The array index referring to a component within the vector (i.e. x, y)</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if the array argument does not contain exactly two components 
        /// </exception>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: { return X; }
                    case 1: { return Y; }
                    default: throw new ArgumentException(TWO_COMPONENTS, "index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: { X = value; break; }
                    case 1: { Y = value; break; }
                    default: throw new ArgumentException(TWO_COMPONENTS, "index");
                }
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Addition of two Vectors
        /// </summary>
        /// <param name="v1">Vector2 to be added to </param>
        /// <param name="v2">Vector2 to be added</param>
        /// <returns>Vector2 representing the sum of two Vectors</returns>
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return
            (
                new Vector2
                    (
                        v1.X + v2.X,
                        v1.Y + v2.Y
                    )
            );
        }

        /// <summary>
        /// Subtraction of two Vectors
        /// </summary>
        /// <param name="v1">Vector2 to be subtracted from </param>
        /// <param name="v2">Vector2 to be subtracted</param>
        /// <returns>Vector2 representing the difference of two Vectors</returns>
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return
            (
                new Vector2
                    (
                        v1.X - v2.X,
                        v1.Y - v2.Y
                    )
            );
        }

        /// <summary>
        /// Product of a Vector2 and a scalar value
        /// </summary>
        /// <param name="v1">Vector2 to be multiplied </param>
        /// <param name="s2">Scalar value to be multiplied by </param>
        /// <returns>Vector2 representing the product of the vector and scalar</returns>
        public static Vector2 operator *(Vector2 v1, double s2)
        {
            return
            (
                new Vector2
                (
                    v1.X * s2,
                    v1.Y * s2
                )
            );
        }

        /// <summary>
        /// Product of a scalar value and a Vector2
        /// </summary>
        /// <param name="s1">Scalar value to be multiplied </param>
        /// <param name="v2">Vector2 to be multiplied by </param>
        /// <returns>Vector2 representing the product of the scalar and Vector2</returns>
        /// <Implementation>
        /// Using the commutative law 'scalar x vector'='vector x scalar'.
        /// Thus, this function calls 'operator*(Vector2 v1, double s2)'.
        /// This avoids repetition of code.
        /// </Implementation>
        public static Vector2 operator *(double s1, Vector2 v2)
        {
            return v2 * s1;
        }

        /// <summary>
        /// Division of a Vector2 and a scalar value
        /// </summary>
        /// <param name="v1">Vector2 to be divided </param>
        /// <param name="s2">Scalar value to be divided by </param>
        /// <returns>Vector2 representing the division of the vector and scalar</returns>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        public static Vector2 operator /(Vector2 v1, double s2)
        {
            return
            (
                new Vector2
                    (
                        v1.X / s2,
                        v1.Y / s2
                    )
            );
        }

        /// <summary>
        /// Negation of a Vector2
        /// Invert the direction of the Vector2
        /// Make Vector2 negative (-vector)
        /// </summary>
        /// <param name="v1">Vector2 to be negated  </param>
        /// <returns>Negated vector</returns>
        public static Vector2 operator -(Vector2 v1)
        {
            return
            (
                new Vector2
                    (
                        -v1.X,
                        -v1.Y
                    )
            );
        }

        /// <summary>
        /// Reinforcement of a Vector2
        /// Make Vector2 positive (+vector)
        /// </summary>
        /// <param name="v1">Vector2 to be reinforced </param>
        /// <returns>Reinforced vector</returns>
        /// <Implementation>
        /// Using the rules of Addition (i.e. '+-x' = '-x' and '++x' = '+x')
        /// This function actually  does nothing but return the argument as given
        /// </Implementation>
        public static Vector2 operator +(Vector2 v1)
        {
            return new Vector2(v1);
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (less than)
        /// </summary>
        /// <param name="v1">Vector2 to be compared </param>
        /// <param name="v2">Vector2 to be compared with</param>
        /// <returns>True if v1 less than v2</returns>
        public static bool operator <(Vector2 v1, Vector2 v2)
        {
            return v1.SumComponentSqrs() < v2.SumComponentSqrs();
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (greater than)
        /// </summary>
        /// <param name="v1">Vector2 to be compared </param>
        /// <param name="v2">Vector2 to be compared with</param>
        /// <returns>True if v1 greater than v2</returns>
        public static bool operator >(Vector2 v1, Vector2 v2)
        {
            return v1.SumComponentSqrs() > v2.SumComponentSqrs();
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (less than or equal to)
        /// </summary>
        /// <param name="v1">Vector2 to be compared </param>
        /// <param name="v2">Vector2 to be compared with</param>
        /// <returns>True if v1 less than or equal to v2</returns>
        public static bool operator <=(Vector2 v1, Vector2 v2)
        {
            return v1.SumComponentSqrs() <= v2.SumComponentSqrs();
        }

        /// <summary>
        /// Compare the magnitude of two Vectors (greater than or equal to)
        /// </summary>
        /// <param name="v1">Vector2 to be compared </param>
        /// <param name="v2">Vector2 to be compared with</param>
        /// <returns>True if v1 greater than or equal to v2</returns>
        public static bool operator >=(Vector2 v1, Vector2 v2)
        {
            return v1.SumComponentSqrs() >= v2.SumComponentSqrs();
        }

        /// <summary>
        /// Compare two Vectors for equality.
        /// Are two Vectors equal.
        /// </summary>
        /// <param name="v1">Vector2 to be compared for equality </param>
        /// <param name="v2">Vector2 to be compared to </param>
        /// <returns>Boolean decision (truth for equality)</returns>
        /// <implementation>
        /// Checks the equality of each pair of components, all pairs must be equal
        /// A tolerence to the equality operator is applied
        /// </implementation>
        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return
            (
                Math.Abs(v1.X - v2.X) <= EqualityTolerence &&
                Math.Abs(v1.Y - v2.Y) <= EqualityTolerence
            );
        }

        /// <summary>
        /// Negative comparator of two Vectors.
        /// Are two Vectors different.
        /// </summary>
        /// <param name="v1">Vector2 to be compared for in-equality </param>
        /// <param name="v2">Vector2 to be compared to </param>
        /// <returns>Boolean decision (truth for in-equality)</returns>
        /// <implementation>
        /// Uses the equality operand function for two vectors to prevent code duplication
        /// </implementation>
        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Determine the dot product of two Vectors
        /// </summary>
        /// <param name="v1">The vector to multiply</param>
        /// <param name="v2">The vector to multiply by</param>
        /// <returns>Scalar representing the dot product of the two vectors</returns>
        /// <implementation>
        /// </implementation>
        public static double DotProduct(Vector2 v1, Vector2 v2)
        {
            return
            (
                v1.X * v2.X +
                v1.Y * v2.Y
            );
        }

        /// <summary>
        /// Determine the dot product of this Vector2 and another
        /// </summary>
        /// <param name="other">The vector to multiply by</param>
        /// <returns>Scalar representing the dot product of the two vectors</returns>
        /// <implementation>
        /// <see cref="DotProduct(Vector2)"/>
        /// </implementation>
        public double DotProduct(Vector2 other)
        {
            return DotProduct(this, other);
        }

        /// <summary>
        /// Get the normalized vector
        /// Get the unit vector
        /// Scale the Vector2 so that the magnitude is 1
        /// </summary>
        /// <param name="v1">The vector to be normalized</param>
        /// <returns>The normalized Vector2</returns>
        /// <implementation>
        /// Uses the Magnitude function to avoid code duplication 
        /// </implementation>
        /// <exception cref="System.DivideByZeroException">
        /// Thrown when the normalisation of a zero magnitude vector is attempted
        /// </exception>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public static Vector2 Normalize(Vector2 v1)
        {
            // Check for divide by zero errors
            if (v1.Magnitude == 0)
            {
                throw new DivideByZeroException(NORMALIZE_0);
            }
            else
            {
                return
                (
                    new Vector2
                    (
                        v1.X / v1.Magnitude,
                        v1.Y / v1.Magnitude
                    )
                );
            }
        }

        /// <summary>
        /// Get the normalized vector
        /// Get the unit vector
        /// Scale the Vector2 so that the magnitude is 1
        /// </summary>
        /// <returns>The normalized Vector2</returns>
        /// <implementation>
        /// Uses the Magnitude and Normalize function to avoid code duplication 
        /// </implementation>
        /// <exception cref="System.DivideByZeroException">
        /// Thrown when the normalisation of a zero magnitude vector is attempted
        /// </exception>
        public Vector2 Normalize()
        {
            this = Normalize(this);
            return this;
        }

        /// <summary>
        /// Take an interpolated value from between two Vectors or an extrapolated value if allowed
        /// </summary>
        /// <param name="v1">The Vector2 to interpolate from (where control ==0)</param>
        /// <param name="v2">The Vector2 to interpolate to (where control ==1)</param>
        /// <param name="control">The interpolated point between the two vectors to retrieve (fraction between 0 and 1), or an extrapolated point if allowed</param>
        /// <param name="allowExtrapolation">True if the control may represent a point not on the vertex between v1 and v2</param>
        /// <returns>The value at an arbitrary distance (interpolation) between two vectors or an extrapolated point on the extended virtex</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when the control is not between values of 0 and 1 and extrapolation is not allowed
        /// </exception>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public static Vector2 Interpolate(Vector2 v1, Vector2 v2, double control, bool allowExtrapolation)
        {
            if (!allowExtrapolation && (control > 1 || control < 0))
            {
                // Error message includes information about the actual value of the argument
                throw new ArgumentOutOfRangeException
                        (
                            "control",
                            control,
                            INTERPOLATION_RANGE + "\n" + ARGUMENT_VALUE + control
                        );
            }
            else
            {
                return
                (
                    new Vector2
                    (
                        v1.X * (1 - control) + v2.X * control,
                        v1.Y * (1 - control) + v2.Y * control
                    )
                );
            }
        }

        /// <summary>
        /// Take an interpolated value from between two Vectors
        /// </summary>
        /// <param name="v1">The Vector2 to interpolate from (where control ==0)</param>
        /// <param name="v2">The Vector2 to interpolate to (where control ==1)</param>
        /// <param name="control">The interpolated point between the two vectors to retrieve (fraction between 0 and 1)</param>
        /// <returns>The value at an arbitrary distance (interpolation) between two vectors</returns>
        /// <implementation>
        /// <see cref="Interpolate(Vector2, Vector2, double, bool)"/>
        /// Uses the Interpolate(Vector2,Vector2,double,bool) method to avoid code duplication
        /// </implementation>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when the control is not between values of 0 and 1
        /// </exception>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public static Vector2 Interpolate(Vector2 v1, Vector2 v2, double control)
        {
            return Interpolate(v1, v2, control, false);
        }


        /// <summary>
        /// Take an interpolated value from between two Vectors
        /// </summary>
        /// <param name="other">The Vector2 to interpolate to (where control ==1)</param>
        /// <param name="control">The interpolated point between the two vectors to retrieve (fraction between 0 and 1)</param>
        /// <returns>The value at an arbitrary distance (interpolation) between two vectors</returns>
        /// <implementation>
        /// <see cref="Interpolate(Vector2, Vector2, double)"/>
        /// Overload for Interpolate method, finds an interpolated value between this Vector2 and another
        /// Uses the Interpolate(Vector2,Vector2,double) method to avoid code duplication
        /// </implementation>
        public Vector2 Interpolate(Vector2 other, double control)
        {
            return Interpolate(this, other, control);
        }

        /// <summary>
        /// Take an interpolated value from between two Vectors or an extrapolated value if allowed
        /// </summary>
        /// <param name="other">The Vector2 to interpolate to (where control ==1)</param>
        /// <param name="control">The interpolated point between the two vectors to retrieve (fraction between 0 and 1), or an extrapolated point if allowed</param>
        /// <param name="allowExtrapolation">True if the control may represent a point not on the vertex between v1 and v2</param>
        /// <returns>The value at an arbitrary distance (interpolation) between two vectors or an extrapolated point on the extended virtex</returns>
        /// <implementation>
        /// <see cref="Interpolate(Vector2, Vector2, double, bool)"/>
        /// Uses the Interpolate(Vector2,Vector2,double,bool) method to avoid code duplication
        /// </implementation>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when the control is not between values of 0 and 1 and extrapolation is not allowed
        /// </exception>
        public Vector2 Interpolate(Vector2 other, double control, bool allowExtrapolation)
        {
            return Interpolate(this, other, control);
        }

        /// <summary>
        /// Find the distance between two Vectors
        /// Pythagoras theorem on two Vectors
        /// </summary>
        /// <param name="v1">The Vector2 to find the distance from </param>
        /// <param name="v2">The Vector2 to find the distance to </param>
        /// <returns>The distance between two Vectors</returns>
        /// <implementation>
        /// </implementation>
        public static double Distance(Vector2 v1, Vector2 v2)
        {
            return
            (
                Math.Sqrt
                (
                    (v1.X - v2.X) * (v1.X - v2.X) +
                    (v1.Y - v2.Y) * (v1.Y - v2.Y)
                )
            );
        }

        /// <summary>
        /// Find the distance between two Vectors
        /// Pythagoras theorem on two Vectors
        /// </summary>
        /// <param name="other">The Vector2 to find the distance to </param>
        /// <returns>The distance between two Vectors</returns>
        /// <implementation>
        /// <see cref="Distance(Vector2, Vector2)"/>
        /// Overload for Distance method, finds distance between this Vector2 and another
        /// Uses the Distance(Vector2,Vector2) method to avoid code duplication
        /// </implementation>
        public double Distance(Vector2 other)
        {
            return Distance(this, other);
        }

        /// <summary>
        /// Find the angle between two Vectors
        /// </summary>
        /// <param name="v1">The Vector2 to discern the angle from </param>
        /// <param name="v2">The Vector2 to discern the angle to</param>
        /// <returns>The angle between two positional Vectors</returns>
        /// <implementation>
        /// </implementation>
        /// <Acknowledgement>F.Hill, 2001, Computer Graphics using OpenGL, 2ed </Acknowledgement>
        public static double Angle(Vector2 v1, Vector2 v2)
        {
            return
            (
                Math.Acos
                    (
                        Normalize(v1).DotProduct(Normalize(v2))
                    )
            );
        }

        /// <summary>
        /// Find the angle between this Vector2 and another
        /// </summary>
        /// <param name="other">The Vector2 to discern the angle to</param>
        /// <returns>The angle between two positional Vectors</returns>
        /// <implementation>
        /// <see cref="Angle(Vector2, Vector2)"/>
        /// Uses the Angle(Vector2,Vector2) method to avoid code duplication
        /// </implementation>
        public double Angle(Vector2 other)
        {
            return Angle(this, other);
        }

        /// <summary>
        /// compares the magnitude of two Vectors and returns the greater Vector2
        /// </summary>
        /// <param name="v1">The vector to compare</param>
        /// <param name="v2">The vector to compare with</param>
        /// <returns>
        /// The greater of the two Vectors (based on magnitude)
        /// </returns>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public static Vector2 Max(Vector2 v1, Vector2 v2)
        {
            if (v1 >= v2) { return v1; }
            return v2;
        }

        /// <summary>
        /// compares the magnitude of two Vectors and returns the greater Vector2
        /// </summary>
        /// <param name="other">The vector to compare with</param>
        /// <returns>
        /// The greater of the two Vectors (based on magnitude)
        /// </returns>
        /// <implementation>
        /// <see cref="Max(Vector2, Vector2)"/>
        /// Uses function Max(Vector2, Vector2) to avoid code duplication
        /// </implementation>
        public Vector2 Max(Vector2 other)
        {
            return Max(this, other);
        }

        /// <summary>
        /// compares the magnitude of two Vectors and returns the lesser Vector2
        /// </summary>
        /// <param name="v1">The vector to compare</param>
        /// <param name="v2">The vector to compare with</param>
        /// <returns>
        /// The lesser of the two Vectors (based on magnitude)
        /// </returns>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public static Vector2 Min(Vector2 v1, Vector2 v2)
        {
            if (v1 <= v2) { return v1; }
            return v2;
        }

        /// <summary>
        /// Compares the magnitude of two Vectors and returns the greater Vector2
        /// </summary>
        /// <param name="other">The vector to compare with</param>
        /// <returns>
        /// The lesser of the two Vectors (based on magnitude)
        /// </returns>
        /// <implementation>
        /// <see cref="Min(Vector2, Vector2)"/>
        /// Uses function Min(Vector2, Vector2) to avoid code duplication
        /// </implementation>
        public Vector2 Min(Vector2 other)
        {
            return Min(this, other);
        }

        /// <summary>
        /// Rotates a Vector2 
        /// Change the roll of a Vector2
        /// </summary>
        /// <param name="v1">The Vector2 to be rotated</param>
        /// <param name="degree">The angle to rotate the Vector2 around in degrees</param>
        /// <returns>Vector2 representing the rotation around the Z axis</returns>
        public static Vector2 Roll(Vector2 v1, double degree)
        {
            double x = (v1.X * Math.Cos(degree)) - (v1.Y * Math.Sin(degree));
            double y = (v1.X * Math.Sin(degree)) + (v1.Y * Math.Cos(degree));
            return new Vector2(x, y);
        }

        /// <summary>
        /// Rotates a Vector2 
        /// Change the roll of a Vector2
        /// </summary>
        /// <param name="degree">The angle to rotate the Vector2 around in degrees</param>
        /// <returns>Vector2 representing the rotation around the Z axis</returns>
        /// <implementation>
        /// <see cref="Roll(Vector2, Double)"/>
        /// Uses function Roll(Vector2, double) to avoid code duplication
        /// </implementation>
        public void Roll(double degree)
        {
            this = Roll(this, degree);
        }

        /// <summary>
        /// Reflect a Vector2 about a given other vector
        /// </summary>
        /// <param name="reflector">The  Vector2 to reflect about</param>
        /// <returns>
        /// The reflected Vector2
        /// </returns>
        public Vector2 Reflection(Vector2 reflector)
        {
            this = Vector2.Reflection(this, reflector);
            return this;
        }

        /// <summary>
        /// Reflect a Vector2 about a given other vector
        /// </summary>
        /// <param name="reflector">The  Vector2 to reflect about</param>
        /// <returns>
        /// The reflected Vector2
        /// </returns>
        public static Vector2 Reflection(Vector2 vector, Vector2 reflector)
        {
            // if reflector has a right angle to vector, return -vector and don't do all
            // the other calculations
            if (Math.Abs(Math.Abs(vector.Angle(reflector)) - Math.PI / 2) < Double.Epsilon)
            {
                return -vector;
            }
            else
            {
                Vector2 retval = new Vector2(2 * vector.Projection(reflector) - vector);
                retval.Magnitude = vector.Magnitude;
                return retval;
            }
        }
        

        /// <summary>
        /// Find the absolute value of a Vector2
        /// Find the magnitude of a Vector2
        /// </summary>
        /// <returns>A Vector2 representing the absolute values of the vector</returns>
        /// <implementation>
        /// An alternative interface to the magnitude property
        /// </implementation>
        public static Double Abs(Vector2 v1)
        {
            return v1.Magnitude;
        }

        /// <summary>
        /// Find the absolute value of a Vector2
        /// Find the magnitude of a Vector2
        /// </summary>
        /// <returns>A Vector2 representing the absolute values of the vector</returns>
        /// <implementation>
        /// An alternative interface to the magnitude property
        /// </implementation>
        public double Abs()
        {
            return this.Magnitude;
        }

        #endregion

        #region Component Operations

        /// <summary>
        /// The sum of a Vector2's components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to sum</param>
        /// <returns>The sum of the Vectors X and Y components</returns>
        public static double SumComponents(Vector2 v1)
        {
            return (v1.X + v1.Y);
        }

        /// <summary>
        /// The sum of this Vector2's components
        /// </summary>
        /// <returns>The sum of the Vectors X, Y and Z components</returns>
        /// <implementation>
        /// <see cref="SumComponents(Vector2)"/>
        /// The Components.SumComponents(Vector2) function has been used to prevent code duplication
        /// </implementation>
        public double SumComponents()
        {
            return SumComponents(this);
        }

        /// <summary>
        /// The sum of a Vector2's squared components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to square and sum</param>
        /// <returns>The sum of the Vectors X^2 and Y^2 components</returns>
        public static double SumComponentSqrs(Vector2 v1)
        {
            Vector2 v2 = SqrComponents(v1);
            return v2.SumComponents();
        }

        /// <summary>
        /// The sum of this Vector2's squared components
        /// </summary>
        /// <returns>The sum of the Vectors X^2 and Y^2</returns>
        /// <implementation>
        /// <see cref="SumComponentSqrs(Vector2)"/>
        /// The Components.SumComponentSqrs(Vector2) function has been used to prevent code duplication
        /// </implementation>
        public double SumComponentSqrs()
        {
            return SumComponentSqrs(this);
        }

        /// <summary>
        /// The individual multiplication to a power of a Vector2's components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to multiply by a power</param>
        /// <param name="power">The power by which to multiply the components</param>
        /// <returns>The multiplied Vector2</returns>
        public static Vector2 PowComponents(Vector2 v1, double power)
        {
            return
            (
                new Vector2
                    (
                        Math.Pow(v1.X, power),
                        Math.Pow(v1.Y, power)
                    )
            );
        }

        /// <summary>
        /// The individual multiplication to a power of this Vector2's components
        /// </summary>
        /// <param name="power">The power by which to multiply the components</param>
        /// <returns>The multiplied Vector2</returns>
        /// <implementation>
        /// <see cref="PowComponents(Vector2, Double)"/>
        /// The Components.PowComponents(Vector2, double) function has been used to prevent code duplication
        /// </implementation>
        public void PowComponents(double power)
        {
            this = PowComponents(this, power);
        }

        /// <summary>
        /// The individual square root of a Vector2's components
        /// </summary>
        /// <param name="v1">The vector whose scalar components to square root</param>
        /// <returns>The rooted Vector2</returns>
        public static Vector2 SqrtComponents(Vector2 v1)
        {
            return
                (
                new Vector2
                    (
                        Math.Sqrt(v1.X),
                        Math.Sqrt(v1.Y)
                    )
                );
        }

        /// <summary>
        /// The individual square root of this Vector2's components
        /// </summary>
        /// <returns>The rooted Vector2</returns>
        /// <implementation>
        /// <see cref="SqrtComponents(Vector2)"/>
        /// The Components.SqrtComponents(Vector2) function has been used to prevent code duplication
        /// </implementation>
        public void SqrtComponents()
        {
            this = SqrtComponents(this);
        }

        /// <summary>
        /// The Vector2's components squared
        /// </summary>
        /// <param name="v1">The vector whose scalar components are to square</param>
        /// <returns>The squared Vector2</returns>
        public static Vector2 SqrComponents(Vector2 v1)
        {
            return Vector2.PowComponents(v1, 2);
        }

        /// <summary>
        /// The Vector2's components squared
        /// </summary>
        /// <returns>The squared Vector2</returns>
        /// <implementation>
        /// <see cref="SqrtComponents(Vector2)"/>
        /// The Components.SqrComponents(Vector2) function has been used to prevent code duplication
        /// </implementation>
        public void SqrComponents()
        {
            this = SqrtComponents(this);
        }


        /// <summary>
        /// Projects the specified v1 onto the specified v2
        /// </summary>
        /// <param name="v1">The vector that will be projected.</param>
        /// <param name="v2">The vector that will be projected upon.</param>
        /// <returns></returns>
        public static Vector2 Projection(Vector2 v1, Vector2 v2)
        {
            // http://de.wikibooks.org/wiki/Ing_Mathematik:_Vektoren#Vektorprojektion
            // http://mathworld.wolfram.com/Reflection.html
            // V1_projectedOn_V2 = v2 * (v1 * v2 / (|v2| ^ 2))

            return new Vector2(
                v2 * ( v1.DotProduct(v2) / Math.Pow(v2.Magnitude, 2) ) 
                );
        }

        /// <summary>
        /// Projects this vector onto the specified v2
        /// </summary>
        /// <param name="v2">The vector that will be projected upon.</param>
        /// <returns></returns>
        public Vector2 Projection(Vector2 v2)
        {
            return Vector2.Projection(this, v2);
        }

        #endregion

        #region Standard Functions

        /// <summary>
        /// Textual description of the Vector2
        /// </summary>
        /// <Implementation>
        /// Uses ToString(string, IFormatProvider) to avoid code duplication
        /// </Implementation>
        /// <returns>Text (String) representing the vector</returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// Verbose textual description of the Vector2
        /// </summary>
        /// <returns>Text (string) representing the vector</returns>
        public string ToVerbString()
        {
            string output = null;

            if (IsUnitVector()) { output += UNIT_VECTOR; }
            else { output += POSITIONAL_VECTOR; }

            output += string.Format("( x={0}, y={1})", X, Y);
            output += MAGNITUDE + Magnitude;

            return output;
        }

        /// <summary>
        /// Textual description of the Vector2
        /// </summary>
        /// <param name="format">Formatting string: 'x','y','z' or '' followed by standard numeric format string characters valid for a double precision floating point</param>
        /// <param name="formatProvider">The culture specific fromatting provider</param>
        /// <returns>Text (String) representing the vector</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            // If no format is passed
            if (format == null || format == "") return String.Format("({0}, {1})", X, Y);

            char firstChar = format[0];
            string remainder = null;

            if (format.Length > 1)
                remainder = format.Substring(1);

            switch (firstChar)
            {
                case 'x': return X.ToString(remainder, formatProvider);
                case 'y': return Y.ToString(remainder, formatProvider);
                default:
                    return String.Format
                        (
                            "({0}, {1})",
                            X.ToString(format, formatProvider),
                            Y.ToString(format, formatProvider)
                        );
            }
        }

        /// <summary>
        /// Get the hashcode
        /// </summary>
        /// <returns>Hashcode for the object instance</returns>
        /// <implementation>
        /// Required in order to implement comparator operations (i.e. ==, !=)
        /// </implementation>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        public override int GetHashCode()
        {
            return
            (
                (int)((X + Y) % Int32.MaxValue)
            );
        }

        /// <summary>
        /// Comparator
        /// </summary>
        /// <param name="other">The other object (which should be a vector) to compare to</param>
        /// <returns>Truth if two vectors are equal within a tolerence</returns>
        /// <implementation>
        /// Checks if the object argument is a Vector2 object 
        /// Uses the equality operator function to avoid code duplication
        /// Required in order to implement comparator operations (i.e. ==, !=)
        /// </implementation>
        public override bool Equals(object other)
        {
            // Check object other is a Vector2 object
            if (other is Vector2)
            {
                // Convert object to Vector2
                Vector2 otherVector = (Vector2)other;

                // Check for equality
                return otherVector == this;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Comparator
        /// </summary>
        /// <param name="other">The other Vector2 to compare to</param>
        /// <returns>Truth if two vectors are equal within a tolerence</returns>
        /// <implementation>
        /// Uses the equality operator function to avoid code duplication
        /// </implementation>
        public bool Equals(Vector2 other)
        {
            return other == this;
        }

        /// <summary>
        /// compares the magnitude of this instance against the magnitude of the supplied vector
        /// </summary>
        /// <param name="other">The vector to compare this instance with</param>
        /// <returns>
        /// -1: The magnitude of this instance is less than the others magnitude
        /// 0: The magnitude of this instance equals the magnitude of the other
        /// 1: The magnitude of this instance is greater than the magnitude of the other
        /// </returns>
        /// <implementation>
        /// Implemented to fulfil the IComparable interface
        /// </implementation>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public int CompareTo(Vector2 other)
        {
            if (this < other)
            {
                return -1;
            }
            else if (this > other)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// compares the magnitude of this instance against the magnitude of the supplied vector
        /// </summary>
        /// <param name="other">The vector to compare this instance with</param>
        /// <returns>
        /// -1: The magnitude of this instance is less than the others magnitude
        /// 0: The magnitude of this instance equals the magnitude of the other
        /// 1: The magnitude of this instance is greater than the magnitude of the other
        /// </returns>
        /// <implementation>
        /// Implemented to fulfil the IComparable interface
        /// </implementation>
        /// <exception cref="ArgumentException">
        /// Throws an exception if the type of object to be compared is not known to this class
        /// </exception>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public int CompareTo(object other)
        {
            if (other is Vector2)
            {
                return CompareTo((Vector2)other);
            }
            else
            {
                // Error condition: other is not a Vector2 object
                throw new ArgumentException
                (
                    // Error message includes information about the actual type of the argument
                    NON_VECTOR_COMPARISON + "\n" + ARGUMENT_TYPE + other.GetType().ToString(),
                    "other"
                );
            }
        }

        #endregion

        #region Decisions

        /// <summary>
        /// Checks if a vector a unit vector
        /// Checks if the Vector2 has been normalized
        /// Checks if a vector has a magnitude of 1
        /// </summary>
        /// <param name="v1">
        /// The vector to be checked for Normalization
        /// </param>
        /// <returns>Truth if the vector is a unit vector</returns>
        /// <implementation>
        /// <see cref="Magnitude"/>	
        /// Uses the Magnitude property in the check to avoid code duplication
        /// Within a tolerence
        /// </implementation>
        public static bool IsUnitVector(Vector2 v1)
        {
            return Math.Abs(v1.Magnitude - 1) <= EqualityTolerence;
        }

        /// <summary>
        /// Checks if the vector a unit vector
        /// Checks if the Vector2 has been normalized 
        /// Checks if the vector has a magnitude of 1
        /// </summary>
        /// <returns>Truth if this vector is a unit vector</returns>
        /// <implementation>
        /// <see cref="IsUnitVector(Vector2)"/>	
        /// Uses the isUnitVector(Vector2) property in the check to avoid code duplication
        /// Within a tolerence
        /// </implementation>
        public bool IsUnitVector()
        {
            return IsUnitVector(this);
        }

        /// <summary>
        /// Checks if a face normal vector represents back face
        /// Checks if a face is visible, given the line of sight
        /// </summary>
        /// <param name="normal">
        /// The vector representing the face normal Vector2
        /// </param>
        /// <param name="lineOfSight">
        /// The unit vector representing the direction of sight from a virtual camera
        /// </param>
        /// <returns>Truth if the vector (as a normal) represents a back face</returns>
        /// <implementation>
        /// Uses the DotProduct function in the check to avoid code duplication
        /// </implementation>
        public static bool IsBackFace(Vector2 normal, Vector2 lineOfSight)
        {
            return normal.DotProduct(lineOfSight) < 0;
        }

        /// <summary>
        /// Checks if a face normal vector represents back face
        /// Checks if a face is visible, given the line of sight
        /// </summary>
        /// <param name="lineOfSight">
        /// The unit vector representing the direction of sight from a virtual camera
        /// </param>
        /// <returns>Truth if the vector (as a normal) represents a back face</returns>
        /// <implementation>
        /// <see cref="Vector2.IsBackFace(Vector2, Vector2)"/> 
        /// Uses the isBackFace(Vector2, Vector2) function in the check to avoid code duplication
        /// </implementation>
        public bool IsBackFace(Vector2 lineOfSight)
        {
            return IsBackFace(this, lineOfSight);
        }

        /// <summary>
        /// Checks if two Vectors are perpendicular
        /// Checks if two Vectors are orthogonal
        /// Checks if one Vector2 is the normal of the other
        /// </summary>
        /// <param name="v1">
        /// The vector to be checked for orthogonality
        /// </param>
        /// <param name="v2">
        /// The vector to be checked for orthogonality to
        /// </param>
        /// <returns>Truth if the two Vectors are perpendicular</returns>
        /// <implementation>
        /// Uses the DotProduct function in the check to avoid code duplication
        /// </implementation>
        public static bool IsPerpendicular(Vector2 v1, Vector2 v2)
        {
            return v1.DotProduct(v2) == 0;
        }

        /// <summary>
        /// Checks if two Vectors are perpendicular
        /// Checks if two Vectors are orthogonal
        /// Checks if one Vector2 is the Normal of the other
        /// </summary>
        /// <param name="other">
        /// The vector to be checked for orthogonality
        /// </param>
        /// <returns>Truth if the two Vectors are perpendicular</returns>
        /// <implementation>
        /// Uses the isPerpendicualr(Vector2, Vector2) function in the check to avoid code duplication
        /// </implementation>
        public bool IsPerpendicular(Vector2 other)
        {
            return IsPerpendicular(this, other);
        }

        #endregion

        #region Cartesian Vectors

        /// <summary>
        /// Vector2 representing the Cartesian origin
        /// </summary>
        public static readonly Vector2 origin = new Vector2(0, 0);

        /// <summary>
        /// Vector2 representing the Cartesian XAxis
        /// </summary>
        public static readonly Vector2 xAxis = new Vector2(1, 0);

        /// <summary>
        /// Vector2 representing the Cartesian YAxis
        /// </summary>
        public static readonly Vector2 yAxis = new Vector2(0, 1);

        #endregion

        #region Messages

        /// <summary>
        /// Exception message descriptive text 
        /// Used for a failure for an array argument to have two components.
        /// </summary>
        private const string TWO_COMPONENTS = "Array must contain exactly two components , (x,y)";

        /// <summary>
        /// Exception message descriptive text 
        /// Used for a divide by zero event caused by the normalization of a vector with magnitude 0 
        /// </summary>
        private const string NORMALIZE_0 = "Can not normalize a vector when it's magnitude is zero";

        /// <summary>
        /// Exception message descriptive text 
        /// Used when interpolation is attempted with a control parameter not between 0 and 1 
        /// </summary>
        private const string INTERPOLATION_RANGE = "Control parameter must be a value between 0 & 1";

        /// <summary>
        /// Exception message descriptive text 
        /// Used when attempting to compare a Vector2 to an object which is not a type of Vector2 
        /// </summary>
        private const string NON_VECTOR_COMPARISON = "Cannot compare a Vector2 to a non-Vector2";

        /// <summary>
        /// Exception message additional information text 
        /// Used when adding type information of the given argument into an error message 
        /// </summary>
        private const string ARGUMENT_TYPE = "The argument provided is a type of ";

        /// <summary>
        /// Exception message additional information text 
        /// Used when adding value information of the given argument into an error message 
        /// </summary>
        private const string ARGUMENT_VALUE = "The argument provided has a value of ";

        /// <summary>
        /// Exception message additional information text 
        /// Used when adding length (number of components in an array) information of the given argument into an error message 
        /// </summary>
        private const string ARGUMENT_LENGTH = "The argument provided has a length of ";

        /// <summary>
        /// Exception message descriptive text 
        /// Used when attempting to set a Vectors magnitude to a negative value 
        /// </summary>
        private const string NEGATIVE_MAGNITUDE = "The magnitude of a Vector2 must be a positive value, (i.e. greater than 0)";

        /// <summary>
        /// Exception message descriptive text 
        /// Used when attempting to set a Vectors magnitude where the Vector2 represents the origin
        /// </summary>
        private const string ORIGIN_VECTOR_MAGNITUDE = "Cannot change the magnitude of Vector2(0,0)";

        ///////////////////////////////////////////////////////////////////////////////

        private const string UNIT_VECTOR = "Unit vector composing of ";

        private const string POSITIONAL_VECTOR = "Positional vector composing of  ";

        private const string MAGNITUDE = " of magnitude ";

        ///////////////////////////////////////////////////////////////////////////////

        #endregion

        #region Constants

        /// <summary>
        /// The tolerence used when determining the equality of two vectors 
        /// </summary>
        public const double EqualityTolerence = Double.Epsilon;

        /// <summary>
        /// The smallest vector possible (based on the double precision floating point structure)
        /// </summary>
        public static readonly Vector2 MinValue = new Vector2(Double.MinValue, Double.MinValue);

        /// <summary>
        /// The largest vector possible (based on the double precision floating point structure)
        /// </summary>
        public static readonly Vector2 MaxValue = new Vector2(Double.MaxValue, Double.MaxValue);

        /// <summary>
        /// The smallest positive (non-zero) vector possible (based on the double precision floating point structure)
        /// </summary>
        public static readonly Vector2 Epsilon = new Vector2(Double.Epsilon, Double.Epsilon);

        #endregion
    }

}
