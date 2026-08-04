"""
CG & IP Project — Hello World Pipeline Test
=============================================
A minimal smoke test that verifies the end-to-end pipeline:
  • Image creation (NumPy)
  • Image processing (OpenCV)
  • GUI window rendering (tkinter)

Run:
    python src/main.py

Expected output:
    A tkinter window showing the original image and edge-detected result
    side by side, confirming the pipeline works correctly.
"""

import cv2
import numpy as np
import tkinter as tk
from tkinter import ttk
from PIL import Image, ImageTk
import os


def main():
    # ── Step 1: Create a dummy image ──────────────────────────────
    img = np.zeros((300, 300, 3), dtype=np.uint8)

    # Draw project title
    cv2.putText(img, 'CG & IP Pipeline OK', (15, 80),
                cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 255, 0), 2)

    # Draw enrollment numbers
    cv2.putText(img, '24000925  24000636  24000859', (10, 150),
                cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 200, 255), 1)

    # Draw team names
    cv2.putText(img, 'Hriday | Sampatti | Zels', (25, 200),
                cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 200, 0), 1)

    # Draw a border rectangle
    cv2.rectangle(img, (5, 5), (295, 295), (100, 100, 255), 2)

    # ── Step 2: Apply baseline image processing ──────────────────
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    edges = cv2.Canny(gray, 100, 200)

    # Save the output image for reference
    edges_bgr = cv2.cvtColor(edges, cv2.COLOR_GRAY2BGR)
    combined = np.hstack([img, edges_bgr])
    output_path = os.path.join(os.path.dirname(__file__), '..', 'pipeline_test_output.png')
    output_path = os.path.abspath(output_path)
    cv2.imwrite(output_path, combined)

    # ── Step 3: Display in tkinter window ────────────────────────
    root = tk.Tk()
    root.title("3D Space Exploration — CG & IP Pipeline Test")
    root.configure(bg='#1a1a2e')
    root.resizable(False, False)

    # Title label
    title_label = tk.Label(
        root,
        text="🚀 3D Space Exploration — Pipeline Test",
        font=("Segoe UI", 16, "bold"),
        fg="#00d4ff",
        bg="#1a1a2e",
        pady=10
    )
    title_label.pack()

    # Subtitle
    subtitle = tk.Label(
        root,
        text="CMP513 + CMP514  |  Computer Graphics & Image Processing",
        font=("Segoe UI", 10),
        fg="#8888aa",
        bg="#1a1a2e",
        pady=2
    )
    subtitle.pack()

    # Frame for images
    img_frame = tk.Frame(root, bg='#1a1a2e', padx=20, pady=10)
    img_frame.pack()

    # Convert OpenCV images to tkinter-compatible format
    # Original image (BGR -> RGB)
    img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    img_pil = Image.fromarray(img_rgb)
    img_tk = ImageTk.PhotoImage(img_pil)

    # Edge-detected image
    edges_pil = Image.fromarray(edges)
    edges_tk = ImageTk.PhotoImage(edges_pil)

    # Left panel - Original Image
    left_frame = tk.Frame(img_frame, bg='#16213e', bd=2, relief='groove')
    left_frame.pack(side=tk.LEFT, padx=10)
    tk.Label(left_frame, text="Original Image", font=("Segoe UI", 11, "bold"),
             fg="#00ff88", bg="#16213e", pady=5).pack()
    tk.Label(left_frame, image=img_tk, bg="#16213e").pack(padx=5, pady=5)

    # Right panel - Edge Detection
    right_frame = tk.Frame(img_frame, bg='#16213e', bd=2, relief='groove')
    right_frame.pack(side=tk.LEFT, padx=10)
    tk.Label(right_frame, text="Canny Edge Detection", font=("Segoe UI", 11, "bold"),
             fg="#ff6b6b", bg="#16213e", pady=5).pack()
    tk.Label(right_frame, image=edges_tk, bg="#16213e").pack(padx=5, pady=5)

    # Status bar
    status = tk.Label(
        root,
        text="✅ Pipeline test PASSED — OpenCV • NumPy • tkinter all working!",
        font=("Segoe UI", 11, "bold"),
        fg="#00ff88",
        bg="#0f3460",
        pady=8,
        padx=15,
        relief='sunken'
    )
    status.pack(fill=tk.X, padx=20, pady=(5, 5))

    # Team info
    team_label = tk.Label(
        root,
        text="Team: Hriday Joshi (24000925) • Sampatti Dave (24000636) • Zels Sorathiya (24000859)",
        font=("Segoe UI", 9),
        fg="#6666aa",
        bg="#1a1a2e",
        pady=8
    )
    team_label.pack()

    print("[OK] Pipeline test passed - all modules working correctly!")
    print(f"[IMG] Output saved to: {output_path}")
    print("[GUI] tkinter window is open - close it to exit.")

    root.mainloop()


if __name__ == '__main__':
    main()
