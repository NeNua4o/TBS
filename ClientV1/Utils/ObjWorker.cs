using ClientV1.Extensions;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClientV1.Utils
{
    public class ObjWorker
    {
        private static ObjWorker _instance;
        public static ObjWorker GetInstance()
        {
            if (_instance == null)
                _instance = new ObjWorker();
            return _instance;
        }

        public bool LoadObj(string filename, out Vector3[] vertices, out Vector2[] uvs, out Vector3[] normals)
        {
            List<int> vertexIndices = new List<int>(), uvIndices = new List<int>(), normalIndices = new List<int>();
            List<Vector3> temp_vertices = new List<Vector3>();
            List<Vector2> temp_uvs = new List<Vector2>();
            List<Vector3> temp_normals = new List<Vector3>();
            vertices = temp_vertices.ToArray();
            uvs = temp_uvs.ToArray();
            normals = temp_normals.ToArray();

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        var tmpStr = sr.ReadLine();
                        if (String.Compare(tmpStr, 0, "vt", 0, 2) == 0)
                        {
                            Vector2 vertex;
                            var str = tmpStr.Replace("vt", "").Replace(".", ",");
                            var vals = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            vertex.X = float.Parse(vals[0]);
                            vertex.Y = float.Parse(vals[1]);
                            temp_uvs.Add(vertex);
                        }
                        
                        else
                        {
                            if (String.Compare(tmpStr, 0, "vn", 0, 2) == 0)
                            {
                                Vector3 vertex;
                                var str = tmpStr.Replace("vn", "").Replace(".", ",");
                                var vals = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                vertex.X = float.Parse(vals[0]);
                                vertex.Y = float.Parse(vals[1]);
                                vertex.Z = float.Parse(vals[2]);
                                temp_normals.Add(vertex);
                            }
                            
                            else
                            {
                                if (String.Compare(tmpStr, 0, "v", 0, 1) == 0)
                                {
                                    Vector3 vertex;
                                    var str = tmpStr.Replace("v", "").Replace(".", ",");
                                    var vals = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    vertex.X = float.Parse(vals[0]);
                                    vertex.Y = float.Parse(vals[1]);
                                    vertex.Z = float.Parse(vals[2]);
                                    temp_vertices.Add(vertex);
                                }
                                else
                                {
                                    if (String.Compare(tmpStr, 0, "f", 0, 1) == 0)
                                    {
                                        List<int> elms = new List<int>();
                                        var str = tmpStr.Replace("f", "").Replace(".", ",");
                                        var vals = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 0; i < vals.Length; i++)
                                        {
                                            var vals2 = vals[i].Split('/');
                                            for (int j = 0; j < vals2.Length; j++)
                                                elms.Add(Int32.Parse(vals2[j]));
                                        }
                                        if (elms.Count != 9)
                                            return false;
                                        vertexIndices.Add(elms[0]);
                                        vertexIndices.Add(elms[3]);
                                        vertexIndices.Add(elms[6]);
                                        uvIndices.Add(elms[1]);
                                        uvIndices.Add(elms[4]);
                                        uvIndices.Add(elms[7]);
                                        normalIndices.Add(elms[2]);
                                        normalIndices.Add(elms[5]);
                                        normalIndices.Add(elms[8]);
                                    }// faces
                                }// normals
                            }// uvs
                        }// vertices

                    }// StreamReader
                    sr.Close();
                    fs.Close();
                }// FileStream
            }

            // Proccess data.
            vertices = new Vector3[vertexIndices.Count];
            for (int i = 0; i < vertexIndices.Count; i++)
            {
                int vertexIndex = vertexIndices[i];
                vertices[i] = temp_vertices[vertexIndex - 1];
            }

            uvs = new Vector2[uvIndices.Count];
            for (int i = 0; i < uvIndices.Count; i++)
            {
                int uvIndex = uvIndices[i];
                uvs[i] = temp_uvs[uvIndex - 1];
                uvs[i].Y = 1 - uvs[i].Y;
            }

            normals = new Vector3[normalIndices.Count];
            for (int i = 0; i < normalIndices.Count; i++)
            {
                int normalIndex = normalIndices[i];
                normals[i] = temp_normals[normalIndex - 1];
            }
            return true;
        }
    }
}
