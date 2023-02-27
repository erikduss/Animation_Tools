﻿// Magica Cloth 2.
// Copyright (c) 2023 MagicaSoft.
// https://magicasoft.jp
using UnityEditor;
using UnityEngine;

namespace MagicaCloth2
{
    public static class GizmoUtility
    {
        // ギズモカラー定義
        public static readonly Color ColorCollider = new Color(0.0f, 1.0f, 0.0f);
        public static readonly Color ColorNonSelectedCollider = new Color(0.5f, 0.3f, 0.0f);
        public static readonly Color ColorSkinningBone = new Color(1.0f, 0.5f, 0.0f);

        public static readonly Quaternion FlipZ = Quaternion.AngleAxis(180.0f, Vector3.up);

        //=========================================================================================
        public static void SetColor(Color col, bool useHandles)
        {
            if (useHandles)
                Handles.color = col;
            else
                Gizmos.color = col;
        }

        public static void DrawSphere(Vector3 pos, float radius, bool useHandles)
        {
            if (useHandles)
            {
                // ソリッド球のサイズ指定は半径ではなく直径なので２倍する
                Handles.SphereHandleCap(0, pos, Quaternion.identity, radius * 2, EventType.Repaint);
            }
            else
                Gizmos.DrawSphere(pos, radius);
        }

        public static void DrawWireSphere(Vector3 pos, Quaternion rot, float radius, Quaternion camRot, bool useHandles)
        {
            if (useHandles)
            {
                Handles.CircleHandleCap(0, pos, camRot, radius, EventType.Repaint);
                Handles.CircleHandleCap(0, pos, rot, radius, EventType.Repaint);
                Handles.CircleHandleCap(0, pos, rot * Quaternion.Euler(90, 0, 0), radius, EventType.Repaint);
                Handles.CircleHandleCap(0, pos, rot * Quaternion.Euler(0, 90, 0), radius, EventType.Repaint);
            }
            else
                Gizmos.DrawWireSphere(pos, radius);
        }

        public static void DrawSimpleWireSphere(Vector3 pos, float radius, Quaternion camRot, bool useHandles)
        {
            if (useHandles)
            {
                Handles.CircleHandleCap(0, pos, camRot, radius, EventType.Repaint);
                //Handles.CircleHandleCap(0, pos, rot, radius, EventType.Repaint);
                //Handles.CircleHandleCap(0, pos, rot * Quaternion.Euler(90, 0, 0), radius, EventType.Repaint);
                //Handles.CircleHandleCap(0, pos, rot * Quaternion.Euler(0, 90, 0), radius, EventType.Repaint);
            }
            else
                Gizmos.DrawWireSphere(pos, radius);
        }

        public static void DrawLine(Vector3 from, Vector3 to, bool useHandles)
        {
            if (useHandles)
                Handles.DrawLine(from, to);
            else
                Gizmos.DrawLine(from, to);
        }

        public static void DrawWireCapsule(Vector3 pos, Quaternion rot, Vector3 dir, Vector3 up, float sradius, float eradius, float len, Quaternion camRot, bool useHandles)
        {
            if (useHandles)
            {
                float slen = len * 0.5f;
                float elen = len * 0.5f;
                slen = Mathf.Max(slen - sradius, 0.0f);
                elen = Mathf.Max(elen - eradius, 0.0f);
                Vector3 sl = dir * slen;
                Vector3 el = -dir * elen;

                //var spos = pos + rot * dir * len * 0.5f;
                //var epos = pos - rot * dir * len * 0.5f;
                var spos = pos + rot * sl;
                var epos = pos + rot * el;

                DrawWireSphere(spos, rot, sradius, camRot, true);
                DrawWireSphere(epos, rot, eradius, camRot, true);
                //Handles.CircleHandleCap(0, spos, camRot, sradius, EventType.Repaint);
                //Handles.CircleHandleCap(0, spos, rot, sradius, EventType.Repaint);
                //Handles.CircleHandleCap(0, spos, rot * Quaternion.Euler(90, 0, 0), sradius, EventType.Repaint);
                //Handles.CircleHandleCap(0, spos, rot * Quaternion.Euler(0, 90, 0), sradius, EventType.Repaint);

                //Handles.CircleHandleCap(0, epos, camRot, eradius, EventType.Repaint);
                //Handles.CircleHandleCap(0, epos, rot, eradius, EventType.Repaint);
                //Handles.CircleHandleCap(0, epos, rot * Quaternion.Euler(90, 0, 0), eradius, EventType.Repaint);
                //Handles.CircleHandleCap(0, epos, rot * Quaternion.Euler(0, 90, 0), eradius, EventType.Repaint);

                //var ps = spos + camRot * Vector3.up * sradius;
                //var es = epos + camRot * Vector3.up * eradius;
                //Handles.DrawLine(ps, es);

                for (int i = 0; i < 360; i += 45) // 45?
                {
                    var q = Quaternion.AngleAxis(i, dir);
                    var up1 = q * (up * sradius);
                    var up2 = q * (up * eradius);
                    Handles.DrawLine(spos + up1, epos + up2);
                }
            }
            else
            {

            }
        }
        //public static void DrawWireCapsule(Vector3 spos, Vector3 epos, Quaternion rot, float sradius, float eradius, Quaternion camRot, bool useHandles)
        //{
        //    if (useHandles)
        //    {
        //        DrawWireSphere(spos, rot, sradius, camRot, true);
        //        DrawWireSphere(epos, rot, eradius, camRot, true);

        //        var ps = spos + camRot * Vector3.up * sradius;
        //        var es = epos + camRot * Vector3.up * eradius;
        //        Handles.DrawLine(ps, es);
        //    }
        //    else
        //    {

        //    }
        //}

        /// <summary>
        /// ワイヤーボックスを描画する
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="size"></param>
        /// <param name="resetMatrix"></param>
        public static void DrawWireCube(Vector3 pos, Quaternion rot, Vector3 size, bool useHandles)
        {
            if (useHandles)
            {
                Handles.DrawWireCube(pos, size); // 何故かカラーが反映しない！バグっぽい
            }
            else
            {
                Gizmos.DrawWireCube(pos, size);
            }
        }


        public static void DrawCross(Vector3 pos, Quaternion rot, float size, bool useHandles)
        {
            if (useHandles)
            {
                Handles.color = Color.red;
                Handles.DrawLine(pos, pos + rot * Vector3.right * size);
                Handles.color = Color.green;
                Handles.DrawLine(pos, pos + rot * Vector3.up * size);
                Handles.color = Color.blue;
                Handles.DrawLine(pos, pos + rot * Vector3.forward * size);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(pos, pos + rot * Vector3.right * size);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(pos, pos + rot * Vector3.up * size);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(pos, pos + rot * Vector3.forward * size);
            }
        }



        public static void DrawCollider(ColliderComponent collider, Quaternion camRot, bool useHandles, bool selected)
        {
            if (collider == null)
                return;

            var cpos = collider.transform.TransformPoint(collider.center);
            var crot = collider.transform.rotation;
            var cscl = Vector3.one * collider.GetScale(); // スケールはx軸のみ（つまり均等スケールのみ）

            // カメラ回転をコライダーのローカル回転に変換
            camRot = Quaternion.Inverse(crot) * camRot;

            // サイズ
            var size = collider.GetSize();

            if (useHandles)
            {
                Handles.matrix = Matrix4x4.TRS(cpos, crot, cscl);
                Handles.color = selected ? ColorCollider : ColorCollider * 0.5f;
                switch (collider.GetColliderType())
                {
                    case ColliderManager.ColliderType.Sphere:
                        DrawWireSphere(Vector3.zero, Quaternion.identity, size.x, camRot, true);
                        break;
                    case ColliderManager.ColliderType.CapsuleX:
                        DrawWireCapsule(Vector3.zero, Quaternion.identity, Vector3.right, Vector3.up, size.x, size.y, size.z, camRot, true);
                        break;
                    case ColliderManager.ColliderType.CapsuleY:
                        DrawWireCapsule(Vector3.zero, Quaternion.identity, Vector3.up, Vector3.right, size.x, size.y, size.z, camRot, true);
                        break;
                    case ColliderManager.ColliderType.CapsuleZ:
                        DrawWireCapsule(Vector3.zero, Quaternion.identity, Vector3.forward, Vector3.up, size.x, size.y, size.z, camRot, true);
                        break;
                    case ColliderManager.ColliderType.Plane:
                        DrawWireCube(Vector3.zero, Quaternion.identity, new Vector3(1.0f, 0.0f, 1.0f) * 1.0f, true);
                        break;
                }
            }
            else
            {

            }
        }



        /// <summary>
        /// ワイヤーカプセルを描画する
        /// UnityのCapsuleColliderと同じ
        /// </summary>
        /// <param name="pos">基準座標</param>
        /// <param name="rot">基準回転</param>
        /// <param name="ldir">カプセルの方向</param>
        /// <param name="lup">カプセルの上方向</param>
        /// <param name="length">カプセルの長さ</param>
        /// <param name="startRadius">始点の半径</param>
        /// <param name="endRadius">終点の半径</param>
        public static void DrawWireCapsule(
            Vector3 pos, Quaternion rot, Vector3 scl,
            Vector3 ldir, Vector3 lup,
            float length, float startRadius, float endRadius,
            bool resetMatrix = true
            )
        {
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, scl);
            float slen = length * 0.5f;
            float elen = length * 0.5f;
            slen = Mathf.Max(slen - startRadius, 0.0f);
            elen = Mathf.Max(elen - endRadius, 0.0f);
            var sl = ldir * slen;
            var el = -ldir * elen;
            Gizmos.DrawWireSphere(sl, startRadius);
            Gizmos.DrawWireSphere(el, endRadius);

            for (int i = 0; i < 360; i += 45)
            {
                var q = Quaternion.AngleAxis(i, ldir);
                var up1 = q * (lup * startRadius);
                var up2 = q * (lup * endRadius);
                Gizmos.DrawLine(sl + up1, el + up2);
            }

            // 45度ずらしてもう１回球を描く
            Gizmos.matrix = Matrix4x4.TRS(pos, rot * Quaternion.AngleAxis(45, ldir), scl);
            Gizmos.DrawWireSphere(sl, startRadius);
            Gizmos.DrawWireSphere(el, endRadius);

            if (resetMatrix)
                Gizmos.matrix = Matrix4x4.identity;
        }

        /// <summary>
        /// ワイヤー球を描画する
        /// UnityのSphereColliderと同じ
        /// </summary>
        /// <param name="pos">基準座標</param>
        /// <param name="rot">基準回転</param>
        /// <param name="radius">半径</param>
        /// <param name="resetMatrix"></param>
        public static void DrawWireSphere(
            Vector3 pos, Quaternion rot, Vector3 scl, float radius,
            bool drawSphere, bool drawAxis,
            bool resetMatrix = true)
        {
            //Gizmos.matrix = Matrix4x4.TRS(pos, rot, Vector3.one);
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, scl);

            // 球
            if (drawSphere)
                Gizmos.DrawWireSphere(Vector3.zero, radius);

            // 軸
            if (drawAxis)
            {
                const float axisRadius = 0.03f;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(Vector3.zero, Vector3.right * axisRadius);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Vector3.zero, Vector3.up * axisRadius);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(Vector3.zero, Vector3.forward * axisRadius);
            }

            // 45度ずらしてもう１回球を描く
            //Gizmos.matrix = Matrix4x4.TRS(pos, rot * Quaternion.AngleAxis(45, Vector3.up), Vector3.one);
            //Gizmos.DrawWireSphere(Vector3.zero, radius);

            if (resetMatrix)
                Gizmos.matrix = Matrix4x4.identity;
        }

        //public static void DrawWireSphere(
        //    Vector3 pos, Quaternion rot, Vector3 scl, float radius,
        //    Quaternion camRot, bool useHandles
        //    )
        //{
        //    if(useHandles)
        //    {
        //        Handles.matrix = Matrix4x4.TRS(pos, rot, scl);

        //    }
        //    else
        //    {

        //    }
        //}


#if false
        /// <summary>
        /// ワイヤーボックスを描画する
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="size"></param>
        /// <param name="resetMatrix"></param>
        public static void DrawWireCube(Vector3 pos, Quaternion rot, Vector3 size, bool resetMatrix = true)
        {
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            if (resetMatrix)
                Gizmos.matrix = Matrix4x4.identity;
        }
#endif

        public static void DrawWireCone(Vector3 pos, Quaternion rot, float length, float radius, int div = 8)
        {
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, Vector3.one);
            var epos = Vector3.forward * length;
            Vector3 oldpos = epos;
            for (int i = 0; i < div; i++)
            {
                float t = (float)i / (float)div;
                var q = Quaternion.AngleAxis(t * 360.0f, Vector3.forward);
                var x = q * Vector3.right * radius;
                Gizmos.DrawLine(Vector3.zero, epos + x);
                Gizmos.DrawLine(epos, epos + x);

                if (i > 0)
                    Gizmos.DrawLine(oldpos, epos + x);

                oldpos = epos + x;
            }

            Gizmos.DrawLine(oldpos, epos + Vector3.right * radius);


            Gizmos.matrix = Matrix4x4.identity;
        }

        /// <summary>
        /// ワイヤー矢印を描画する
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="size"></param>
        /// <param name="cross">十字描画</param>
        public static void DrawWireArrow(Vector3 pos, Quaternion rot, Vector3 size, bool cross = false)
        {
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, size);

            Vector3[] points = new Vector3[]
            {
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.5f, -1.0f),
                new Vector3(0.0f, 0.5f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
            };

            float addAngle = cross ? 90.0f : 180.0f;
            int loop = cross ? 4 : 2;

            for (int j = 0; j < loop; j++)
            {
                for (int i = 0; i < points.Length - 1; i++)
                {
                    Gizmos.DrawLine(points[i], points[i + 1]);
                }

                rot = rot * Quaternion.AngleAxis(addAngle, Vector3.forward);
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, size);
            }

            Gizmos.matrix = Matrix4x4.identity;
        }

        /// <summary>
        /// XYZ軸を描画する
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="size"></param>
        /// <param name="resetMatrix"></param>
        public static void DrawAxis(Vector3 pos, Quaternion rot, float size, bool resetMatrix = true)
        {
            Gizmos.matrix = Matrix4x4.TRS(pos, rot, Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Vector3.zero, Vector3.right * size);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(Vector3.zero, Vector3.up * size);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(Vector3.zero, Vector3.forward * size);
            if (resetMatrix)
                Gizmos.matrix = Matrix4x4.identity;
        }

        /// <summary>
        /// ボーン形状を描画する
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="tpos"></param>
        /// <param name="size"></param>
        public static void DrawBone(Vector3 pos, Vector3 tpos, float size)
        {
            var v = tpos - pos;
            var rot = Quaternion.FromToRotation(Vector3.forward, v);

            Gizmos.matrix = Matrix4x4.TRS(pos, rot, Vector3.one);
            Gizmos.color = ColorSkinningBone;

            Gizmos.DrawWireSphere(Vector3.zero, size);

            //Gizmos.DrawLine(Vector3.zero, Vector3.forward * v.magnitude);
            float bsize = size * 0.8f;
            float zoff = size;
            var gpos = Vector3.forward * v.magnitude;
            var p0 = new Vector3(bsize, bsize, zoff);
            var p1 = new Vector3(bsize, -bsize, zoff);
            var p2 = new Vector3(-bsize, -bsize, zoff);
            var p3 = new Vector3(-bsize, bsize, zoff);

            Gizmos.DrawLine(p0, gpos);
            Gizmos.DrawLine(p1, gpos);
            Gizmos.DrawLine(p2, gpos);
            Gizmos.DrawLine(p3, gpos);

            Gizmos.DrawLine(p0, p1);
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p0);

            Gizmos.matrix = Matrix4x4.identity;
        }

        //=========================================================================================
        /// <summary>
        /// Handlesによるコーンの描画
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="size"></param>
        /// <param name="angle">角度(deg)</param>
        /// <param name="coneColor"></param>
        /// <param name="wireColor"></param>
        /// <param name="wireThickness"></param>
        public static void ConeHandle(
            Vector3 pos, Quaternion rot, float size, float angle,
            Color coneColor, Color wireColor, float wireThickness = 1.0f,
            int controllId = 0
            )
        {
            // デフォルトのコーン描画は角度や直径が決まっているので描画角度によりそれをスケーリングさせる
            // サイズ１のオリジナルコーンの情報
            // 底面の半径0.4, 直径0.8
            // 高さ1.2（中心から底面まで0.5, 中心から始点まで0.7)
            // 角度18.434948度

            //float size = scr.size;
            float rad = Mathf.Deg2Rad * angle;
            float z = Mathf.Cos(rad);
            float xy = Mathf.Sin(rad);

            // 角度18.434948の倍率
            const float cosBaseScl = 1.0f / 0.94868329f;
            const float sinBaseScl = 1.0f / 0.31622775f;
            z *= cosBaseScl;
            xy *= sinBaseScl;
            //Debug.Log(z);

            float zoffset = size * 0.7f;

            //readonly Quaternion flipQ = Quaternion.AngleAxis(180.0f, Vector3.up);

            //Handles.matrix = Matrix4x4.TRS(Vector3.zero, rot, new Vector3(xy, xy, z));
            Handles.matrix = Matrix4x4.TRS(pos, rot * FlipZ, new Vector3(xy, xy, z));
            //var pos = transform.position;

            // cone
            Handles.color = coneColor;
            Vector3 offset = new Vector3(0.0f, 0.0f, -zoffset);
            Handles.ConeHandleCap(
                controllId,
                //pos + offset,
                offset,
                Quaternion.identity,
                size,
                EventType.Repaint
            );

            // wire dist
            Handles.color = wireColor;
            Handles.DrawWireDisc(
                //pos + new Vector3(0.0f, 0.0f, -size * 1.2f),
                new Vector3(0.0f, 0.0f, -size * 1.2f),
                Vector3.forward,
                size * (0.4f / 1.0f),
                wireThickness
                );

            // wire
            //Handles.color = scr.wireColor;
            //var spos = pos;
            const int div = 6;
            const float angStep = 360.0f / div;
            for (int i = 0; i < div; i++)
            {
                float ang = Mathf.Deg2Rad * i * angStep;
                float x = Mathf.Sin(ang);
                float y = Mathf.Cos(ang);
                x *= size * (0.4f / 1.0f);
                y *= size * (0.4f / 1.0f);
                //Handles.DrawLine(spos, new Vector3(x, y, -size * 1.2f), wireThickness);
                Handles.DrawLine(Vector3.zero, new Vector3(x, y, -size * 1.2f), wireThickness);
            }

            Handles.matrix = Matrix4x4.identity;
        }

        /// <summary>
        /// 扇を描画する
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="angle"></param>
        /// <param name="size"></param>
        /// <param name="wireThickness"></param>
        /// <param name="arcColor1"></param>
        /// <param name="arcColor2"></param>
        /// <param name="wireColor"></param>
        public static void ArcHandle(
            Vector3 pos, Quaternion rot, float angle, float size, float wireThickness,
            Color arcColor1, Color arcColor2, Color wireColor
            )
        {
            var up = rot * Vector3.up;
            var right = rot * Vector3.right;
            var forward = rot * Vector3.forward;

            Handles.color = arcColor1;
            Handles.DrawSolidArc(pos, up, forward, angle, size);
            Handles.DrawSolidArc(pos, -up, forward, angle, size);

            Handles.color = arcColor2;
            Handles.DrawSolidArc(pos, right, forward, angle, size);
            Handles.DrawSolidArc(pos, -right, forward, angle, size);

            Handles.color = wireColor;
            Handles.DrawLine(pos, pos + forward * size, wireThickness);

        }
    }
}
