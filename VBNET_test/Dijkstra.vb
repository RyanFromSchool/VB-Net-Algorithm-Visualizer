Public Class Dijkstra
    Dim rand As New Random
    Dim myGraphics As Graphics = Me.CreateGraphics
    Private Sub DrawNode(ByVal thisNode As Node)
        Dim penNode As Pen
        penNode = New Pen(Drawing.Color.Maroon, 20)
        myGraphics.DrawRectangle(penNode, thisNode.X, thisNode.Y, 1, 1)
        Threading.Thread.Sleep(150)
        penNode.Dispose()
    End Sub

    Private Sub DrawEdge(ByVal thisEdge As Edge, ByVal color As Drawing.Color, ByVal width As Integer)
        Dim penEdge As Pen
        penEdge = New Pen(color, width)
        myGraphics.DrawLine(penEdge, x1:=thisEdge.getA().X, y1:=thisEdge.getA().Y, x2:=thisEdge.getB().X, y2:=thisEdge.getB().Y)
        Threading.Thread.Sleep(100)
        penEdge.Dispose()
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click
        BtnStart.Hide()
        Dim numNodes As Integer = rand.Next(5, 20)
        Dim randomEdgeGraph As New Graph(numNodes)

        For Each node In randomEdgeGraph.GetNodes()
            DrawNode(node)
        Next
        For Each edge In randomEdgeGraph.GetEdges()
            DrawEdge(edge, Color.Crimson, 2)
        Next

        ' Dijkstra's Algorithm
        Dim D_alg As New Algorithm(randomEdgeGraph)
        Dim D_sol As Integer() = D_alg.Dijkstra(0)



        For Each i In D_sol
            'DrawEdge(i, Color.CornflowerBlue, 4)
        Next
    End Sub



End Class


Public Class Node
    Public X As Integer
    Public Y As Integer
    Public visited As Boolean
    Public Sub New(ByVal X As Integer, ByVal Y As Integer)
        visited = False
        Me.X = X
        Me.Y = Y
    End Sub
End Class
Public Class Edge
    Private A As Node
    Private B As Node
    Private AIndex As Integer
    Private BIndex As Integer
    Private weight As Double
    Public Sub New()
        Me.A = New Node(0, 0)
        Me.B = New Node(0, 0)
        Me.weight = -1
        Me.AIndex = 0
        Me.BIndex = 0
    End Sub
    Public Sub New(ByVal A As Node, ByVal B As Node, ByVal weight As Double)
        Me.A = A
        Me.B = B
        Me.weight = weight
        Me.AIndex = 0
        Me.BIndex = 0
    End Sub

    Public Sub New(ByVal A As Node, ByVal B As Node, ByVal weight As Double, ByVal srcIndex As Integer, ByVal destIndex As Integer)
        Me.A = A
        Me.B = B
        Me.weight = weight
        Me.AIndex = srcIndex
        Me.BIndex = destIndex
    End Sub

    Public Function getWeight()
        Return weight
    End Function

    Public Function getA()
        Return Me.A
    End Function

    Public Function getB()
        Return Me.B
    End Function

    Public Function getBIndex()
        Return Me.BIndex
    End Function

    Public Function getAIndex()
        Return Me.AIndex
    End Function

    Public Sub setWeight(ByVal weight As Double)
        Me.weight = weight
    End Sub

    Public Sub setA(ByVal A As Node)
        Me.A = A
    End Sub

    Public Sub setB(ByVal B As Node)
        Me.B = B
    End Sub

    Public Sub setAIndex(ByVal AIndex As Integer)
        Me.AIndex = AIndex
    End Sub

    Public Sub setBIndex(ByVal BIndex As Integer)
        Me.BIndex = BIndex
    End Sub


End Class
Public Class Graph
    Dim rand As New Random
    Private nodes() As Node
    Private edges() As Edge
    Private adjacencyMatrix(,) As Integer
    Public numNodes = 0
    Dim numEdges = 0

    Public Sub New(ByVal numNodes As Integer)
        nodes = GenerateNodes(numNodes)
        ReDim adjacencyMatrix(numNodes - 1, numNodes - 1)
        Me.numNodes = nodes.Length

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
        Dim maxEdges As Integer = (numNodes * (numNodes - 1) / 2)
        Dim minEdges As Integer = numNodes - 1
        numEdges = rand.Next(minEdges, maxEdges)
        ReDim edges(numEdges - 1)
        edges = GenerateEdges()
    End Sub

    Public Sub New(ByVal nodes As Node())
        Me.nodes = nodes
        numNodes = nodes.Length
        ReDim adjacencyMatrix(numNodes, numNodes)
        Me.numNodes = nodes.Length

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
        Dim maxEdges As Integer = (numNodes * (numNodes - 1) / 2)
        Dim minEdges As Integer = numNodes - 1
        numEdges = rand.Next(minEdges, maxEdges)
        ReDim edges(numEdges - 1)
        edges = GenerateEdges()

    End Sub

    Public Sub New(ByVal nodes() As Node, ByVal edges() As Edge)
        Me.nodes = nodes
        Me.edges = edges
        ReDim adjacencyMatrix(numNodes, numNodes)
        numNodes = nodes.Length
        numEdges = edges.Length

        Dim x, y As Integer
        For x = 0 To numNodes - 1
            For y = 0 To numNodes - 1
                adjacencyMatrix(x, y) = 0
            Next
        Next
    End Sub
    Public Function GenerateNodes(numNodes As Integer) As Node()
        Dim nodes(numNodes - 1) As Node
        Dim CoordinateMax As Integer = 200
        For index As Integer = 0 To numNodes - 1
            Dim coordinateX As Integer = GenRandCoord(CoordinateMax)
            Dim coordinateY As Integer = GenRandCoord(CoordinateMax)
            Dim thisNode As New Node(coordinateX, coordinateY)
            nodes(index) = thisNode
        Next
        Return nodes
    End Function
    Public Function GenerateEdges() As Edge()
        Dim sourceNodeIdx, destNodeIdx As Integer
        Dim src, dest As Node
        Dim nodeIndices As List(Of Integer)

        'Something is wrong
        ' once: 6 nodes 1 edge
        For i As Integer = 0 To numEdges - 1
            nodeIndices = Enumerable.Range(0, numNodes).ToList()
            sourceNodeIdx = rand.Next(0, nodeIndices.Count)
            nodeIndices.RemoveAt(sourceNodeIdx)
            destNodeIdx = rand.Next(0, nodeIndices.Count)
            src = nodes(sourceNodeIdx)
            dest = nodes(destNodeIdx)
            edges(i) = New Edge(src, dest, GetDistanceToOrigin(src, dest), sourceNodeIdx, destNodeIdx)
            AddEdgeToMatrix(edges(i))
        Next

        Return edges
    End Function
    Public Sub AddNode(ByVal x As Integer, ByVal y As Integer)
        ReDim nodes(nodes.Length)
        nodes(nodes.Length - 1) = New Node(x, y)
    End Sub

    Private Sub AddEdgeToMatrix(edge As Edge)
        adjacencyMatrix(edge.getAIndex, edge.getBIndex) = edge.getWeight()
        adjacencyMatrix(edge.getBIndex, edge.getAIndex) = edge.getWeight()
    End Sub

    Private Function GenRandCoord(ByVal MAX_NUM As Integer)
        Return rand.Next(1, MAX_NUM)
    End Function

    Private Function GetDistanceToOrigin(srcNode As Node, destNode As Node)
        Return Math.Sqrt((Math.Abs(destNode.X - srcNode.X) ^ 2) + (Math.Abs(destNode.Y - srcNode.Y) ^ 2))
    End Function

    Friend Function GetEdges() As IEnumerable(Of Object)
        Return edges
    End Function

    Friend Function GetNodes() As IEnumerable(Of Object)
        Return nodes
    End Function

    Friend Function GetAdjacencyMatrix() As Integer(,)
        Return adjacencyMatrix
    End Function
End Class
Public Class Algorithm
    Private distance As Integer()
    Private shortestPathTreeSet As Boolean()
    Private adjacencyMatrix As Integer(,)
    Private graph As Graph
    Private numNodes = New Integer
    Private nodeIndices = New Integer()

    Public Sub New(ByVal graph As Graph)
        Me.graph = graph
        distance = New Integer(graph.numNodes - 1) {}
        shortestPathTreeSet = New Boolean(graph.numNodes - 1) {}
        adjacencyMatrix = graph.GetAdjacencyMatrix()
        ReDim nodeIndices(graph.numNodes - 1)
    End Sub

    Public Function Dijkstra(source As Integer) As Integer()
        numNodes = graph.numNodes
        ' sptSet is all False from declaration
        For i As Integer = 0 To numNodes - 1
            distance(i) = Integer.MaxValue
        Next

        distance(source) = 0

        For count As Integer = 0 To numNodes - 2
            Dim MinDistIdx As Integer = MinimumDistance(distance, shortestPathTreeSet, numNodes)
            shortestPathTreeSet(MinDistIdx) = True

            For thisNode As Integer = 0 To numNodes - 1
                If Not shortestPathTreeSet(thisNode) AndAlso Convert.ToBoolean(adjacencyMatrix(MinDistIdx, thisNode)) AndAlso distance(MinDistIdx) <> Integer.MaxValue AndAlso distance(MinDistIdx) + adjacencyMatrix(MinDistIdx, thisNode) < distance(thisNode) Then
                    distance(thisNode) = distance(MinDistIdx) + adjacencyMatrix(MinDistIdx, thisNode)
                    nodeIndices(thisNode) = MinDistIdx 'ERROR
                End If
            Next
        Next

        Print(distance, numNodes)
        Return distance
    End Function

    Private Sub Print(distance As Integer(), numNodes As Integer)
        Debug.WriteLine("Vertex    Distance from source")

        For i As Integer = 0 To numNodes - 1
            Debug.WriteLine("{0}" & vbTab & "  {1}", i, distance(i))
        Next
    End Sub


    Private Function MinimumDistance(distance As Integer(), shortestPathTreeSet As Boolean(), numNodes As Integer) As Integer
        Dim min As Integer = Integer.MaxValue
        Dim minIndex As Integer = 0

        For v As Integer = 0 To numNodes - 1
            If shortestPathTreeSet(v) = False AndAlso distance(v) <= min Then
                min = distance(v)
                minIndex = v
            End If
        Next

        Return minIndex
    End Function

    Public Function getAdjacencyMatrix()
        Return adjacencyMatrix
    End Function

End Class


