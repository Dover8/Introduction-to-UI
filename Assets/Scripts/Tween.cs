/*
 * Copyright (c) 2021 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using UnityEngine;

public static class Tween
{
    static float duration = 1f;
    public static void Lerp(this MonoBehaviour m, Transform from, Transform to)
    {
        Lerp(m, from, to, duration);
    }

    public static void Lerp(this MonoBehaviour m, Transform from, Transform to, float time)
    {
        Transform transform = m.transform;
        transform.position = from.position;
        transform.rotation = from.rotation;

        m.StartCoroutine(Lerp(transform, to, time));
    }

    private static IEnumerator Lerp(Transform transform, Transform target, float time)
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPos, target.position, elapsedTime / time);
            transform.rotation = Quaternion.Lerp(startRot, target.rotation, elapsedTime / time);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public static void Lerp(this MonoBehaviour m, Vector2 start, Vector2 end, float time = 1f)
    {
        if (m.GetComponent<RectTransform>() != null)
        {
            m.StartCoroutine(Lerp(m.GetComponent<RectTransform>(), start, end, time));
        }
    }

    private static IEnumerator Lerp(RectTransform rt, Vector2 start, Vector2 end, float time)
    {
        float elapsedTime = 0;
        rt.anchoredPosition = start;

        while (elapsedTime < time)
        {
            rt.anchoredPosition = Vector2.LerpUnclamped(start, end, elapsedTime / time);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
