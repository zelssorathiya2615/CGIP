"""
test_pipeline.py
================
Basic unit tests to verify the pipeline components work independently.
"""

import unittest
import numpy as np


class TestPipelineComponents(unittest.TestCase):
    """Test that core dependencies are importable and functional."""

    def test_numpy_array_creation(self):
        """Verify NumPy can create an image-like array."""
        img = np.zeros((300, 300, 3), dtype=np.uint8)
        self.assertEqual(img.shape, (300, 300, 3))
        self.assertEqual(img.dtype, np.uint8)

    def test_opencv_import(self):
        """Verify OpenCV is installed and importable."""
        import cv2
        self.assertIsNotNone(cv2.__version__)

    def test_opencv_color_conversion(self):
        """Verify OpenCV can perform basic color conversion."""
        import cv2
        img = np.zeros((100, 100, 3), dtype=np.uint8)
        gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        self.assertEqual(gray.shape, (100, 100))

    def test_opencv_edge_detection(self):
        """Verify OpenCV Canny edge detection works."""
        import cv2
        img = np.zeros((100, 100), dtype=np.uint8)
        edges = cv2.Canny(img, 100, 200)
        self.assertEqual(edges.shape, (100, 100))

    def test_matplotlib_import(self):
        """Verify matplotlib is installed and importable."""
        import matplotlib
        self.assertIsNotNone(matplotlib.__version__)

    def test_pyopengl_import(self):
        """Verify PyOpenGL is installed and importable."""
        import OpenGL
        self.assertIsNotNone(OpenGL.__version__)


if __name__ == '__main__':
    unittest.main()
