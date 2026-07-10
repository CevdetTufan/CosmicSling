using System;
using System.Drawing;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using CosmicSling.Application.Services;
using CosmicSling.Domain.Enums;
using CosmicSling.Domain.ValueObjects;
using CosmicSling.Infrastructure.Persistence;
using CosmicSling.Presentation.Rendering;

namespace CosmicSling.Presentation;

public class GameForm : Form
{
    private readonly SKControl _skControl;
    private readonly GameSessionService _session = new();
    private readonly IHighScoreRepository _highScoreRepo = new InMemoryHighScoreRepository();

    private readonly SpaceshipRenderer _shipRenderer = new();
    private readonly CelestialRenderer _celestialRenderer = new();
    private readonly PortalRenderer _portalRenderer = new();
    private readonly ObstacleRenderer _obstacleRenderer = new();
    private readonly TrajectoryRenderer _trajectoryRenderer = new();
    private readonly HudRenderer _hudRenderer = new();

    private readonly System.Windows.Forms.Timer _gameTimer;
    private bool _isAimingDrag;
    private Vector2D _dragStart;
    private Vector2D _dragCurrent;

    public GameForm()
    {
        Text = "Cosmic Sling - C# Clean Architecture (.NET 10 & SkiaSharp)";
        ClientSize = new Size(1200, 800);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview = true;

        _skControl = new SKControl
        {
            Dock = DockStyle.Fill
        };
        _skControl.PaintSurface += OnPaintSurface;
        _skControl.MouseDown += OnCanvasMouseDown;
        _skControl.MouseMove += OnCanvasMouseMove;
        _skControl.MouseUp += OnCanvasMouseUp;

        Controls.Add(_skControl);

        KeyDown += OnFormKeyDown;

        _session.LoadLevel(1);

        _gameTimer = new System.Windows.Forms.Timer { Interval = 16 };
        _gameTimer.Tick += (s, e) =>
        {
            _session.Update(0.016f);
            if (_session.CurrentState == GameState.LevelCompleted)
            {
                _highScoreRepo.SaveBestAttempts(_session.CurrentLevel.LevelIndex, _session.Attempts);
            }
            _skControl.Invalidate();
        };
        _gameTimer.Start();
    }

    private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(new SKColor(13, 17, 23)); // Sleek dark space background

        var level = _session.CurrentLevel;

        // Draw Portal
        _portalRenderer.Render(canvas, level.Portal);

        // Draw Celestial Bodies
        foreach (var body in level.CelestialBodies)
        {
            _celestialRenderer.Render(canvas, body);
        }

        // Draw Obstacles
        foreach (var obstacle in level.Obstacles)
        {
            _obstacleRenderer.Render(canvas, obstacle);
        }

        // Draw Spaceship
        _shipRenderer.Render(canvas, level.Ship);

        // Draw Aiming Trajectory
        if (_isAimingDrag && _session.CurrentState == GameState.Aiming)
        {
            var launchVel = CalculateLaunchVelocity();
            var trajectory = _session.GetPredictedTrajectory(launchVel);
            _trajectoryRenderer.Render(canvas, trajectory);
        }

        // Draw HUD
        var best = _highScoreRepo.GetBestAttempts(level.LevelIndex);
        _hudRenderer.Render(canvas, _session, best);
    }

    private Vector2D CalculateLaunchVelocity()
    {
        var diff = _dragStart - _dragCurrent;
        return diff * 2.5f; // Slingshot power multiplier
    }

    private void OnCanvasMouseDown(object? sender, MouseEventArgs e)
    {
        if (_session.CurrentState == GameState.Aiming && e.Button == MouseButtons.Left)
        {
            _isAimingDrag = true;
            _dragStart = new Vector2D(e.X, e.Y);
            _dragCurrent = _dragStart;
        }
    }

    private void OnCanvasMouseMove(object? sender, MouseEventArgs e)
    {
        if (_isAimingDrag)
        {
            _dragCurrent = new Vector2D(e.X, e.Y);
        }
    }

    private void OnCanvasMouseUp(object? sender, MouseEventArgs e)
    {
        if (_isAimingDrag && e.Button == MouseButtons.Left)
        {
            _isAimingDrag = false;
            var launchVel = CalculateLaunchVelocity();
            _session.LaunchShip(launchVel);
        }
    }

    private void OnFormKeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.R:
                _session.ResetLevel();
                break;
            case Keys.Z:
                _session.UndoLastCommand();
                break;
            case Keys.D1:
                _session.LoadLevel(1);
                break;
            case Keys.D2:
                _session.LoadLevel(2);
                break;
            case Keys.D3:
                _session.LoadLevel(3);
                break;
        }
    }
}
